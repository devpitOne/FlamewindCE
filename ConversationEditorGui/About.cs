using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConversationEditorGui
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            newLink.Links.Add(0,newLink.Text.Length, "https://neverwintervault.org/project/nwnee/other/tool/flamewind-conversation-editor-20");
            HelpLink.Links.Add(0, HelpLink.Text.Length, "http://www.flamewind.com");
            this.HelpText.Text = "This program and the associated Neverwinter\nNights 2 plugin were originally created by Cassandra Gelvin,\nan amateur programmer in southern California.";
        }

        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink(e.Link.LinkData);
            }
            catch
            {
                MessageBox.Show("Unable to open clicked link.");
            }
        }

        private void VisitLink(object link)
        {
            this.HelpLink.LinkVisited = true;
            System.Diagnostics.Process.Start(link.ToString());
        }
    }
}