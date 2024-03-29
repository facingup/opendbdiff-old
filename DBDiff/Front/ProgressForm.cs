﻿using System;
using System.Windows.Forms;
using DBDiff.Schema.Events;
using DBDiff.Schema.SQLServer.Generates.Generates;
using DBDiff.Schema.SQLServer.Generates.Model;

namespace DBDiff.Front
{
    public partial class ProgressForm : Form
    {
        private Generate genData1;
        private Generate genData2;
        private bool IsProcessing = false;
        private Database origen = null;
        private Database destino = null;
        private Database origenClone = null;
        
        // TODO: thread-safe error reporting
        private Exception error;
        private string friendlyErrorLocation;
        private string mostRecentProgressMessage;

        public ProgressForm(string DatabaseName1, string DatabaseName2, Generate genData1, Generate genData2)
        {
            InitializeComponent();
            databaseProgressControl1.Maximum = Generate.MaxValue;
            databaseProgressControl2.Maximum = Generate.MaxValue;
            databaseProgressControl1.DatabaseName = DatabaseName1;
            databaseProgressControl2.DatabaseName = DatabaseName2;
            this.genData1 = genData1;
            this.genData2 = genData2;
        }

        public Database Source
        {
            get { return origen; }
        }

        public Database Destination
        {
            get { return destino; }
        }

        public string ErrorLocation
        {
            get { return this.friendlyErrorLocation; }
        }

        public string ErrorMostRecentProgress
        {
            get { return this.mostRecentProgressMessage; }
        }

        public Exception Error
        {
            get { return this.error; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Close();
            this.Cursor = Cursors.Default;
        }

        private void ProgressForm_Activated(object sender, EventArgs e)
        {
            var handler = new ProgressEventHandler.ProgressHandler(genData2_OnProgress);
            try
            {
                if (!IsProcessing)
                {
                    this.Refresh();
                    IsProcessing = false;
                    genData1.OnProgress += new ProgressEventHandler.ProgressHandler(genData1_OnProgress);
                    genData2.OnProgress += handler;
                    Generate.OnCompareProgress += handler;

                    /*Thread t1 = new Thread(delegate()
                    {*/
                    this.friendlyErrorLocation = "Loading " + databaseProgressControl1.DatabaseName;
                    origen = genData1.Process();
                    databaseProgressControl2.Message = "Complete";
                    databaseProgressControl2.Value = Generate.MaxValue;
                    /*});
                    Thread t2 = new Thread(delegate()
                    {*/
                    this.friendlyErrorLocation = "Loading " + databaseProgressControl2.DatabaseName;
                    destino = genData2.Process();

                    origenClone = (Database)origen.Clone(null);
                    /*});
                    t1.Start();
                    t2.Start();
                    t1.Join();
                    t2.Join();
                    */
                    this.friendlyErrorLocation = "Comparing Databases";
                    destino = Generate.Compare(origen, destino);
                    origen = origenClone;

                    databaseProgressControl1.Message = "Complete";
                    databaseProgressControl1.Value = Generate.MaxValue;
                }
            }
            catch (Exception err)
            {
                this.error = err;
            }
            finally
            {
                Generate.OnCompareProgress -= handler;
                this.Dispose();
            }
        }

        void genData2_OnProgress(ProgressEventArgs e)
        {
            if (e.Progress > -1 && databaseProgressControl1.Value != e.Progress)
            {
                databaseProgressControl1.Value = e.Progress;
            }

            if (String.Compare(databaseProgressControl1.Message, e.Message) != 0)
            {
                databaseProgressControl1.Message = e.Message;
            }

            this.mostRecentProgressMessage = e.Message;
        }

        void genData1_OnProgress(ProgressEventArgs e)
        {
            if (e.Progress > -1 && databaseProgressControl2.Value != e.Progress)
            {
                databaseProgressControl2.Value = e.Progress;
            }

            if (String.Compare(databaseProgressControl2.Message, e.Message) != 0)
            {
                databaseProgressControl2.Message = e.Message;
            }

            this.mostRecentProgressMessage = e.Message;
        }
    }
}
