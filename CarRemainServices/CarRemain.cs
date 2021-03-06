﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Util;
using System.Data.SqlClient;
//using System.Windows.Forms;
using System.Timers;

namespace CarRemainServices
{
    public partial class CarRemain : ServiceBase
    {
        private string CS = Properties.Settings.Default.connectionstring.ToString();
        private string QueryDependency = Properties.Settings.Default.QueryChange.ToString();
        private string QueryPlaceCorrect = Properties.Settings.Default.QueryPlaceCorrect.ToString();

        private string QueryCar = Properties.Settings.Default.QueryCar.ToString();
        private string QueryMotor = Properties.Settings.Default.QueryMotor.ToString();
        private string QueryVIPPlace = Properties.Settings.Default.QueryVIPPlace.ToString();
        private int InitialInterval = Properties.Settings.Default.TimerInterval;
        private string ParkingSlot = Properties.Settings.Default.FilePath.ToString();
        private int UpdateTimeInterval = Properties.Settings.Default.UpdateTimeInterval;

        // public string connectionString = Properties.Settings.Default.connectionstring.ToString();
        public string _LOG_DIRECTORY;

        public int NoCar = 0;
        public int NoMotor = 0;
        public int DefaultNoCar = 0;
        public int DefaultNoMotor = 0;
        public int CorrectCar = 0;
        public int CorrectMotor = 0;
        public int CarSlotRemain = 0;
        public int MotorSlotRemain = 0;
        public DateTime LastUpdatetime = new DateTime();

        public CarRemain()
        {
            InitializeComponent();
            _LOG_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + "\\LOG\\";
            
        }

        protected override void OnStart(string[] args)
        {
            WriteLog("Program Start");
            #region Timer
            System.Timers.Timer InitializeTimer = new System.Timers.Timer();
            InitializeTimer.Elapsed += new ElapsedEventHandler(GetInitialData);
            InitializeTimer.Interval = InitialInterval;
            InitializeTimer.Enabled = true;

            #endregion

            InitializeComponent();
           
            GetVIPPlace();
            GetPlaceCorrectNum();
            StartCarChecking();
        }

        private void GetInitialData(object sender, ElapsedEventArgs e)
        {
            try
            {
                WriteLog("Get Initial Data");
                GetVIPPlace();
                GetPlaceCorrectNum();

                DateTime currenttime = DateTime.Now.ToLocalTime();
                TimeSpan span = currenttime.Subtract(LastUpdatetime);
                if (span.Minutes >= UpdateTimeInterval)
                {
                    WriteLog("No Any Update in " + UpdateTimeInterval.ToString() + " Mins");
                    WriteLog("Update Parking Slot");
                    CountCarSlot();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        

        protected override void OnStop()
        {
            WriteLog("Program End");
        }

        public void WriteLog(string inMessage)
        {
            if (!Directory.Exists(_LOG_DIRECTORY))
            {
                try
                {
                    Directory.CreateDirectory(_LOG_DIRECTORY);
                }
                catch (Exception ex)
                {
                }
            }
            string strLogFile = _LOG_DIRECTORY + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_CarSlotRemain.log";

            inMessage = "(" + DateTime.Now.ToShortTimeString() + ") : " + inMessage;

            //Logger.Instance.InitFileLogger(strLogFile);
            Logger.Instance.InitFilelogger(strLogFile);
            Logger.Instance.Writelog(strLogFile, inMessage);
        }

        private void StartCarChecking()
        {
            try
            {
                SqlConnection cn = null;
                cn = new SqlConnection(CS);

                SqlDataAdapter CarIn = new SqlDataAdapter(QueryDependency, cn);
                SqlDataAdapter PlaceCorrect = new SqlDataAdapter(QueryPlaceCorrect, cn);

                SqlDependency dependency = new SqlDependency(CarIn.SelectCommand);
                dependency.OnChange += this.OnNotificationChange;

                SqlDependency depPlaceCorrect = new SqlDependency(PlaceCorrect.SelectCommand);
                depPlaceCorrect.OnChange += this.OnNotificationChange;

                SqlDependency.Start(CS);

                DataSet ds = new DataSet();
                CarIn.Fill(ds);

                DataSet dscorrect = new DataSet();
                PlaceCorrect.Fill(dscorrect);

                CountCarSlot();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            // this.Invoke(new UICallback(FillGrid)); 
        }
        private void OnNotificationChange(object sender, SqlNotificationEventArgs e)
        {
            //this.Invoke(new UICallback(FillGrid));
            // MessageBox.Show("Change");
            try
            {
                StartCarChecking();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        private void CountCarSlot()
        {
            try
            {
                NoCar = GetNoOfCar();
                NoMotor = GetNoOfMotor();
                CarSlotRemain = (DefaultNoCar - CorrectCar) - NoCar;
                MotorSlotRemain = (DefaultNoMotor - CorrectMotor) - NoMotor;


                WriteLog("Car :" + CarSlotRemain.ToString() + " Motor: " + MotorSlotRemain.ToString());
                //WriteLog();
                LastUpdatetime = DateTime.Now.ToLocalTime();
                OutputFile();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        private void OutputFile()
        {
            try
            {
                Int64 x;
                string output = "";
                if (!File.Exists(ParkingSlot))
                {

                    StreamWriter sw = new StreamWriter(ParkingSlot, true, Encoding.ASCII);
                    output = "S:" + CarSlotRemain.ToString() + ":" + MotorSlotRemain.ToString() + ":E";
                    sw.Write(output);

                    sw.WriteLine();
                    //close the file
                    sw.Close();
                }
                else
                {
                    File.Delete(ParkingSlot);
                    StreamWriter sw = new StreamWriter(ParkingSlot, true, Encoding.ASCII);
                    output = "S:" + CarSlotRemain.ToString() + ":" + MotorSlotRemain.ToString() + ":E";
                    sw.Write(output);

                    sw.WriteLine();
                    //close the file
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }


        private int GetNoOfCar()
        {

            int NoOfCar = 0;
            DataTable table = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(QueryCar, cn);
                    cmd.CommandType = CommandType.Text;

                    table.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    cmd = null;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    WriteLog(e.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            NoOfCar = table.Rows.Count;
            return NoOfCar;
        }
        private int GetNoOfMotor()
        {
            int NoOfMotor = 0;
            DataTable table = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(QueryMotor, cn);
                    cmd.CommandType = CommandType.Text;

                    table.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    cmd = null;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    WriteLog(e.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            NoOfMotor = table.Rows.Count;
            return NoOfMotor;
        }

        private void GetVIPPlace()
        {
            int NoOfCar = 0;

            DataTable table = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(QueryVIPPlace, cn);
                    cmd.CommandType = CommandType.Text;

                    table.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    cmd = null;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    WriteLog(e.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            DefaultNoCar = Convert.ToInt32(table.Rows[0]["CarAllPlaceNum"]);
            DefaultNoMotor = Convert.ToInt32(table.Rows[0]["BikeAllPlaceNum"]);
        }

        private void GetPlaceCorrectNum()
        {
            int NoOfCar = 0;

            DataTable table = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(QueryPlaceCorrect, cn);
                    cmd.CommandType = CommandType.Text;

                    table.Load(cmd.ExecuteReader());
                    cmd.Dispose();
                    cmd = null;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    WriteLog(e.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            CorrectCar = Convert.ToInt32(table.Rows[0]["CorrectNum"]);
            CorrectMotor = Convert.ToInt32(table.Rows[0]["BikeCorrectNum"]);
        }

    }
}
