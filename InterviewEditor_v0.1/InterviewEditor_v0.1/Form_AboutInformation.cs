// ========================================================================================================================================================================================================
// Project: Interview Editor
// File: Form_AboutInformation
// Creator: Alicia Yeargin
// ========================================================================================================================================================================================================

using System;
using System.Windows.Forms;

namespace InterviewEditor_v0._1
{
    public partial class Form_AboutInformation : Form
    {
        // ----------------------------------------------------------------------------------------------------
        // INITIALIZATION
        // ----------------------------------------------------------------------------------------------------
        public Form_AboutInformation()
        {
            InitializeComponent();

            // Set the creator email link to be a mailto link.
            label_link_CreatorEmail.Links[0].LinkData = "mailto:aliciayeargin@gmail.com";
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Opens the website for the creator.
        // ----------------------------------------------------------------------------------------------------
        void openCreatorWebsite()
        {
            System.Diagnostics.Process.Start(label_link_CreatorWebsite.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Opens email link for the creator.
        // ----------------------------------------------------------------------------------------------------
        void emailCreator()
        {
            System.Diagnostics.Process.Start("mailto:" + label_link_CreatorEmail.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Copies the URL for the Git to the clipboard.
        // ----------------------------------------------------------------------------------------------------
        void copyGitURL()
        {
            Clipboard.SetText(label_link_Git.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Opens the URL for the Git.
        // ----------------------------------------------------------------------------------------------------
        void openGitURL()
        {
            System.Diagnostics.Process.Start(label_link_Git.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Copies the URL for the Wiki.
        // ----------------------------------------------------------------------------------------------------
        void copyWikiURL()
        {
            Clipboard.SetText(label_link_Wiki.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Opens the URL for the Wiki.
        // ----------------------------------------------------------------------------------------------------
        void openWikiURL()
        {
            System.Diagnostics.Process.Start(label_link_Wiki.Text);
        }
        // ----------------------------------------------------------------------------------------------------

        // Event: Creator Website 'Open' button
        private void button_Creator_Open_Click(object sender, EventArgs e)
        {
            openCreatorWebsite();
        }

        // Event: Creator 'Email' button
        private void button_Creator_Email_Click(object sender, EventArgs e)
        {
            emailCreator();
        }

        // Event: Git 'Copy' button.
        private void button_Git_Copy_Click(object sender, EventArgs e)
        {
            copyGitURL();
        }

        // Event: Git 'Open' button.
        private void button_Git_Open_Click(object sender, EventArgs e)
        {
            openGitURL();
        }

        // Event: Wiki 'Copy' button.
        private void button_Wiki_Copy_Click(object sender, EventArgs e)
        {
            copyWikiURL();
        }

        // Event: Wiki 'Open' button.
        private void button_Wiki_Open_Click(object sender, EventArgs e)
        {
            openWikiURL();
        }

        // Event: Click on creator website link.
        private void label_link_CreatorWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openCreatorWebsite();
        }

        // Event: Click on creator email link.
        private void label_link_CreatorEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            emailCreator();
        }

        // Event: Click on Git link.
        private void label_link_Git_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openGitURL();
        }

        // Event: Click on Wiki link.
        private void label_link_Wiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openWikiURL();
        }
    }
}
