﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iSpyApplication.Controls
{
    public partial class Filter : Form
    {
        public static List<int> CheckedCameraIDs = new List<int>();
        public static List<int> CheckedMicIDs = new List<int>();
        public static DateTime StartDate = DateTime.Now.AddDays(-7);
        public static DateTime EndDate = DateTime.Now;
        public static bool Filtered = false;

        public Filter()
        {
            InitializeComponent();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckedCameraIDs = new List<int>();
            CheckedMicIDs = new List<int>();
            for(int i=0;i<clbObjects.Items.Count;i++)
            {
                var o = (Li)clbObjects.Items[i];
                if (clbObjects.GetItemCheckState(i)== CheckState.Checked)
                {
                    if (o.Ot==1)
                    {
                        CheckedMicIDs.Add(o.ID);
                    }
                    if (o.Ot==2)
                    {
                        CheckedCameraIDs.Add(o.ID);
                    }
                }
            }
            StartDate = dateTimePicker1.Value;
            EndDate = dateTimePicker2.Value;
            Filtered = chkFilter.Checked;

            DialogResult = DialogResult.OK;
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            foreach(var c in MainForm.Cameras)
            {
                var l = new Li {Name = c.name, Ot = 2, Selected = false, ID=c.id};
                if (CheckedCameraIDs.Contains(c.id))
                    l.Selected = true;
                clbObjects.Items.Add(l);
                clbObjects.SetItemCheckState(clbObjects.Items.Count - 1, l.Selected ? CheckState.Checked : CheckState.Unchecked);
            }
            foreach (var c in MainForm.Microphones)
            {
                var l = new Li { Name = c.name, Ot = 1, Selected = false, ID=c.id };
                if (CheckedMicIDs.Contains(c.id))
                    l.Selected = true;
                clbObjects.Items.Add(l);
                clbObjects.SetItemCheckState(clbObjects.Items.Count - 1, l.Selected?CheckState.Checked : CheckState.Unchecked);
                
            }

            dateTimePicker1.Value = StartDate;
            dateTimePicker2.Value = EndDate;
            chkFilter.Checked = Filtered;
            tlpFilter.Enabled = chkFilter.Checked;

        }

        private struct Li
        {
            public string Name;
            public int Ot;
            public int ID;
            public bool Selected;
            public override string ToString()
            {
                return Name;
            }


        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            tlpFilter.Enabled = chkFilter.Checked;
        }
    }
}