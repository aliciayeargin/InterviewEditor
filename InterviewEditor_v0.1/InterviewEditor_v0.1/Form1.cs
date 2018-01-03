using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

/* 
 * TODO: 
 * - Add a settings/preferences menu.
 */

namespace InterviewEditor_v0._1
{
    public partial class Form_MainEditor : Form
    {
        // ========================================================================================================================================================================================================
        // DECLARATIONS
        // ========================================================================================================================================================================================================

        // ----------------------------------------------------------------------------------------------------
        // Class: StoryBeat
        // ----------------------------------------------------------------------------------------------------
        public class StoryBeat
        {
            // Members.
            public int id;
            public string background_image;
            public string character_image;
            public StoryBeatType type;
            public string main_text;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Class: StoryBeatNarrative (inherits from Story Beat)
        // ----------------------------------------------------------------------------------------------------
        public class StoryBeatNarrative : StoryBeat
        {
            // Constructor, importing beat data.
            public StoryBeatNarrative(string[] data)
            {
                // Assign all of the members with what's provided in the data array.
                id = Convert.ToInt32(data[0]);
                background_image = data[1];
                character_image = data[2];
                type = (StoryBeatType)Enum.Parse(typeof(StoryBeatType), data[3]);
                main_text = data[4];
                next_id = Convert.ToInt32(data[5]);
            }

            // Constructor, converting from Choice beat.
            public StoryBeatNarrative(StoryBeatChoice oldBeat)
            {
                // Assign all of the members with what's provided in the data array.
                id = oldBeat.id;
                background_image = oldBeat.background_image;
                character_image = oldBeat.character_image;
                type = StoryBeatType.Narrative;
                main_text = oldBeat.main_text;
                next_id = -1;
            }

            // Constructor, adding new beat.
            public StoryBeatNarrative(int newID)
            {
                id = newID;
                background_image = "none";
                character_image = "none";
                type = StoryBeatType.Narrative;
                main_text = "__NEW__";
                next_id = -1;
            }

            // Members.
            public int next_id;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Class: StoryBeatChoice (inherits from StoryBeat)
        // ----------------------------------------------------------------------------------------------------
        public class StoryBeatChoice : StoryBeat
        {
            // Constructor, importing beat data.
            public StoryBeatChoice(string[] data)
            {
                // Assign all of the members with what's provided in the data array.
                id = Convert.ToInt32(data[0]);
                background_image = data[1];
                character_image = data[2];
                type = (StoryBeatType)Enum.Parse(typeof(StoryBeatType), data[3]);
                main_text = data[4];

                int dataIndex = 5;

                // Add all of the choices.
                for (int i = 0; i < 3; ++i)
                {
                    // Initialize the choice object.
                    choices[i] = new StoryChoice();

                    // Set the choice ID. (1, 2, 3)
                    choices[i].choice_id = Convert.ToInt32(data[dataIndex]);
                    ++dataIndex;

                    // Set the text for the choice.
                    choices[i].choice_text = data[dataIndex];
                    ++dataIndex;

                    // Set the choice's next ID.
                    choices[i].next_id = Convert.ToInt32(data[dataIndex]);
                    ++dataIndex;
                }
            }

            // Constructor, converting from Narrative beat.
            public StoryBeatChoice(StoryBeatNarrative oldBeat)
            {
                // Assign all of the members with what's provided in the data array.
                id = oldBeat.id;
                background_image = oldBeat.background_image;
                character_image = oldBeat.character_image;
                type = StoryBeatType.Choice;
                main_text = oldBeat.main_text;

                // Add all of the choices with blank data.
                for (int i = 0; i < 3; ++i)
                {
                    // Initialize the choice object.
                    choices[i] = new StoryChoice();

                    // Set the choice ID. (1, 2, 3)
                    choices[i].choice_id = i + 1;

                    // Set the text for the choice.
                    choices[i].choice_text = "__NEW__";

                    // Set the choice's next ID.
                    choices[i].next_id = -1;
                }
            }

            // Constructor, adding new beat.
            public StoryBeatChoice(int newID)
            {
                // Assign all of the members with blank data.
                id = newID;
                background_image = "none";
                character_image = "none";
                type = StoryBeatType.Choice;
                main_text = "__NEW__";

                // Add all of the choices.
                for (int i = 0; i < 3; ++i)
                {
                    // Initialize the choice object.
                    choices[i] = new StoryChoice();

                    // Set the choice ID. (1, 2, 3)
                    choices[i].choice_id = i + 1;

                    // Set the text for the choice.
                    choices[i].choice_text = "__NEW__";

                    // Set the choice's next ID.
                    choices[i].next_id = -1;
                }
            }

            // Members.
            public StoryChoice[] choices = new StoryChoice[3];
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Class: StoryChoice
        // ----------------------------------------------------------------------------------------------------
        public class StoryChoice
        {
            public int choice_id;
            public string choice_text;
            public int next_id;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Members: Form1
        // ----------------------------------------------------------------------------------------------------

        // Enum for story beat types.
        public enum StoryBeatType
        {
            Narrative,
            Choice
        }

        List<StoryBeat> storyBeatList;      // List of all of the story beats.
        List<ComboBox> selectIDFieldsList;  // List of all of the combo boxes where you can select an ID.
        List<Form> windowList;              // List of all the windows that have been opened.
        int currentListIndex;               // The list index of the current story beat being displayed.
        bool firstLoad;                     // Keep track of whether or not we're in the first loading state.
        string filePathName;                // The path and name of the file loaded in.
        string localFolderPath;             // The path to whatever the local folder is.
        string settingsFileName = "InterviewEditor_Settings.txt";   // The file name for the .txt file with the saved settings.
        string helpFilename = "InterviewEditor_Help.txt";           // The file name for the .txt file with the help information.

        System.Windows.Controls.TextBox textbox_General_MainText;
        System.Windows.Controls.TextBox textbox_Choice_ChoiceText_1;
        System.Windows.Controls.TextBox textbox_Choice_ChoiceText_2;
        System.Windows.Controls.TextBox textbox_Choice_ChoiceText_3;

        // ----------------------------------------------------------------------------------------------------

        // ========================================================================================================================================================================================================
        // INITIALIZATION
        // ========================================================================================================================================================================================================

        public Form_MainEditor()
        {
            InitializeComponent();

            // Initialize the text boxes.
            textbox_General_MainText = new System.Windows.Controls.TextBox();
            textbox_General_MainText.SpellCheck.IsEnabled = true;
            elementHost_General_MainText.Child = textbox_General_MainText;

            textbox_Choice_ChoiceText_1 = new System.Windows.Controls.TextBox();
            textbox_Choice_ChoiceText_1.SpellCheck.IsEnabled = true;
            elementHost_Choice_ChoiceText_1.Child = textbox_Choice_ChoiceText_1;

            textbox_Choice_ChoiceText_2 = new System.Windows.Controls.TextBox();
            textbox_Choice_ChoiceText_2.SpellCheck.IsEnabled = true;
            elementHost_Choice_ChoiceText_2.Child = textbox_Choice_ChoiceText_2;

            textbox_Choice_ChoiceText_3 = new System.Windows.Controls.TextBox();
            textbox_Choice_ChoiceText_3.SpellCheck.IsEnabled = true;
            elementHost_Choice_ChoiceText_3.Child = textbox_Choice_ChoiceText_3;

            // Add event listeners to fields.
            textbox_Menu_Find.KeyDown += searchboxHitEnterKey;
            textbox_General_BackgroundImage.Leave += textbox_General_BackgroundImage_Leave;
            textbox_General_CharacterImage.Leave += textbox_General_CharacterImage_Leave;
            comboBox_General_BeatType.SelectedIndexChanged += comboBox_General_BeatType_SelectedIndexChanged;
            textbox_General_MainText.LostFocus += textbox_General_MainText_Leave;
            comboBox_Narrative_NextID.SelectedIndexChanged += comboBox_Narrative_NextID_SelectedIndexChanged;
            comboBox_Choice_NextID_1.SelectedIndexChanged += comboBox_Choice_NextID_1_SelectedIndexChanged;
            comboBox_Choice_NextID_2.SelectedIndexChanged += comboBox_Choice_NextID_2_SelectedIndexChanged;
            comboBox_Choice_NextID_3.SelectedIndexChanged += comboBox_Choice_NextID_3_SelectedIndexChanged;
            textbox_Choice_ChoiceText_1.LostFocus += textbox_Choice_ChoiceText_1_Leave;
            textbox_Choice_ChoiceText_2.LostFocus += textbox_Choice_ChoiceText_2_Leave;
            textbox_Choice_ChoiceText_3.LostFocus += textbox_Choice_ChoiceText_3_Leave;
            promptToSaveOnExitToolStripMenuItem.Click += changeSettingStripMenuItem_Click;
            caseSensitiveSearchToolStripMenuItem.Click += changeSettingStripMenuItem_Click;
            this.FormClosing += Form1_FormClosing;

            // Enter for the textboxes.
            textbox_General_BackgroundImage.KeyDown += textboxHitEnterKey;
            textbox_General_CharacterImage.KeyDown += textboxHitEnterKey;
            textbox_General_MainText.KeyDown += UITextboxHitEnterKey;
            textbox_Choice_ChoiceText_1.KeyDown += UITextboxHitEnterKey;
            textbox_Choice_ChoiceText_2.KeyDown += UITextboxHitEnterKey;
            textbox_Choice_ChoiceText_3.KeyDown += UITextboxHitEnterKey;

            // Disable the panels since there is no file loaded.
            panel_Selection.Enabled = false;
            panel_General.Enabled = false;
            panel_Narrative.Enabled = false;
            panel_Choice.Enabled = false;
            panel_Previous.Enabled = false;
            panel_Next.Enabled = false;
            textbox_Menu_Find.Enabled = false;
            button_Menu_Find.Enabled = false;

            // Initialize the list for the story beats.
            storyBeatList = new List<StoryBeat>();
            currentListIndex = 0;
            filePathName = "";
            firstLoad = true;

            // Initialize the list of the ID comboboxes.
            selectIDFieldsList = new List<ComboBox>();

            // Add all of the ID comboboxes to the list.
            selectIDFieldsList.Add(comboBox_Selection);
            selectIDFieldsList.Add(comboBox_Narrative_NextID);
            selectIDFieldsList.Add(comboBox_Choice_NextID_1);
            selectIDFieldsList.Add(comboBox_Choice_NextID_2);
            selectIDFieldsList.Add(comboBox_Choice_NextID_3);

            // Get the path for the local folder.
            localFolderPath = AppDomain.CurrentDomain.BaseDirectory;

            // Adjust the menu items to properly display the shortcuts
            nextIDToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Plus";
            previousIDToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Minus";

            // Initialize the list for keeping track of opened windows.
            windowList = new List<Form>();

            // DEBUG: Load the example file.
            if (File.Exists(localFolderPath + "example.txt"))
            {
                filePathName = localFolderPath + "example.txt";
                Text = "Interview Editor (v0.1) - " + filePathName;
                loadFile();
            }
            // END DEBUG.
        }

        // ========================================================================================================================================================================================================
        // FUNCTIONS
        // ========================================================================================================================================================================================================

        // ----------------------------------------------------------------------------------------------------
        // Function: Create a new file.
        // ----------------------------------------------------------------------------------------------------
        void newFile()
        {
            SaveFileDialog selectFileDialog = new SaveFileDialog();
            selectFileDialog.Title = "New File";
            selectFileDialog.Filter = "text files (*.txt)|*.txt";

            // Provide dialog and check to make sure that the user selects a file.
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the name of the file that was selected.
                filePathName = selectFileDialog.FileName;

                // Update the window title to include the file name.
                Text = "Interview Editor (v0.1) - " + filePathName;

                // Write a new file.
                File.WriteAllText(filePathName, "100000|default|none|Narrative|Sample text.|-1");

                // Load the new file.
                loadFile();
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Export data to current file.
        // ----------------------------------------------------------------------------------------------------
        void saveFile()
        {
            // Check to see if a file hasn't been selected yet.
            if (filePathName == "")
            {
                SaveFileDialog selectFileDialog = new SaveFileDialog();
                selectFileDialog.Filter = "text files (*.txt)|*.txt";

                // Provide dialog and check to make sure that the user selects a file.
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the name of the file that was selected.
                    filePathName = selectFileDialog.FileName;

                    // Update the title for the window to include the filename.
                    Text = "Interview Editor (v0.1) - " + filePathName;
                }

                // Otherwise, no file was selected, so don't do anything.
                else
                {
                    return;
                }
            }

            // Make sure to include changes from the currently open ID.
            updateBeatListFromFields();

            // Write all of the beats from the beat list to a string.
            File.WriteAllText(filePathName, createStoryBeatListString());

            label_statusStrip.Text = "Successfully saved to file: " + getFileName();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Saves all of the settings out to the settings file.
        // ----------------------------------------------------------------------------------------------------
        void saveSettingsFile()
        {
            // Gather all of the settings up into a single string.
            string settings;

            // Save prompt setting.
            settings = "[Prompt to save on exit]=";

            // Check to see if the save prompt setting is enabled.
            if (promptToSaveOnExitToolStripMenuItem.Checked == true)
            {
                // Add true to the setting string.
                settings += "true";
            }

            // Otherwise, it's not enabled.
            else
            {
                // Add false to the setting string.
                settings += "false";
            }

            // Case sensitive searches setting.
            settings += "\n[Case sensitive searches]=";

            if (caseSensitiveSearchToolStripMenuItem.Checked == true)
            {
                settings += "true";
            }
            else
            {
                settings += "false";
            }

            // Write all of the settings to the settings file.
            File.WriteAllText(settingsFileName, settings);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Creates a string for exporting the story beat list.
        // ----------------------------------------------------------------------------------------------------
        string createStoryBeatListString()
        {
            string export = "";

            // Go through the whole story beat list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Add all of the General data.
                export += storyBeatList[i].id.ToString() + "|";
                export += storyBeatList[i].background_image + "|";
                export += storyBeatList[i].character_image + "|";
                export += storyBeatList[i].type.ToString() + "|";
                export += storyBeatList[i].main_text + "|";
                
                // Check if the current beat is a Narrative one.
                if (storyBeatList[i].type == StoryBeatType.Narrative)
                {
                    // Add the Narrative data.
                    export += ((StoryBeatNarrative)storyBeatList[i]).next_id.ToString();
                }

                // Check if the current beat is a Choice one.
                else if(storyBeatList[i].type == StoryBeatType.Choice)
                {
                    // Add the Choice data for choice 1.
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[0].choice_id.ToString() + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[0].choice_text + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[0].next_id.ToString() + "|";

                    // Add the Choice data for choice 2.
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[1].choice_id.ToString() + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[1].choice_text + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[1].next_id.ToString() + "|";

                    // Add the Choice data for choice 3.
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[2].choice_id.ToString() + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[2].choice_text + "|";
                    export += ((StoryBeatChoice)storyBeatList[i]).choices[2].next_id.ToString();
                }

                // Check to make sure it's not the last line.
                if (i != storyBeatList.Count - 1)
                {
                    // Add a newline.
                    export += "\n";
                }
            }

            return export;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: File select dialog and export data.
        // ----------------------------------------------------------------------------------------------------
        void saveNewFile()
        {
            SaveFileDialog selectFileDialog = new SaveFileDialog();
            selectFileDialog.Filter = "text files (*.txt)|*.txt";

            // Provide dialog and check to make sure that the user selects a file.
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the name of the file that was selected.
                filePathName = selectFileDialog.FileName;

                // Update the title of the window with the file name.
                Text = "Interview Editor (v0.1) - " + filePathName;

                // Save out to the selected file.
                saveFile();
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Clears everything to prepare for loading a new file (when there's one loaded already).
        // ----------------------------------------------------------------------------------------------------
        void clearPreviousLoad()
        {
            // Clear out the story beat list.
            storyBeatList.Clear();

            // Reset the current list index.
            currentListIndex = 0;

            // Set first load back to true.
            firstLoad = true;

            // Clear all beat fields.
            clearBeatFields();

            // Clear items from the comboboxes.
            comboBox_Selection.Items.Clear();
            comboBox_Narrative_NextID.Items.Clear();
            comboBox_Choice_NextID_1.Items.Clear();
            comboBox_Choice_NextID_2.Items.Clear();
            comboBox_Choice_NextID_3.Items.Clear();

            // Close all of the windows in the window list.
            for (int i = 0; i < windowList.Count; ++i)
            {
                windowList[i].Close();
                windowList[i].Dispose();
            }

            // Clear out the window list.
            windowList.Clear();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: File select dialog for the load file.
        // ----------------------------------------------------------------------------------------------------
        void loadFileDialog()
        {
            OpenFileDialog loadFileDialog = new OpenFileDialog();
            loadFileDialog.Filter = "text files (*.txt)|*.txt";

            // Provide dialog and check to make sure that the user selects a file.
            if (loadFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the name of the file that was selected.
                filePathName = loadFileDialog.FileName;

                // Update the window title with the file name.
                Text = "Interview Editor (v0.1) - " + filePathName;

                // Load the selected file.
                loadFile();
            }

        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: File select dialog and import data.
        // ----------------------------------------------------------------------------------------------------
        void loadFile()
        {
            // Check to make sure that the file exists.
            if (File.Exists(filePathName) == false)
            {
                label_statusStrip.Text = "ERROR: Unable to find file: " + filePathName;
                return;
            }

            // Clear out everything from the previous load.
            clearPreviousLoad();

            // Load in the settings file.
            loadSettingsFile();

            // Get all of the lines of text from the file.
            string[] lines = File.ReadAllLines(filePathName);

            // Get the character for the pipe, which is used to split the elements of a dialog object.
            char splitter = Convert.ToChar("|");

            // Go through all of the lines.
            for (int i = 0; i < lines.Length; ++i)
            {
                // Split up all of the elements of the current line.
                string[] elements = lines[i].Split(splitter);

                // Check to make sure there are at least 4 elements.
                if (elements.Length > 3)
                {
                    // Narrative object.
                    if (elements[3] == "Narrative")
                    {
                        storyBeatList.Add(new StoryBeatNarrative(elements));
                    }

                    // Choice object.
                    else if (elements[3] == "Choice")
                    {
                        storyBeatList.Add(new StoryBeatChoice(elements));
                    }
                }
            }

            // Update the status message.
            label_statusStrip.Text = "Successfully loaded file: " + getFileName();

            // Enable the first panels.
            panel_Selection.Enabled = true;
            panel_General.Enabled = true;
            panel_Previous.Enabled = true;
            panel_Next.Enabled = true;
            textbox_Menu_Find.Enabled = true;
            button_Menu_Find.Enabled = true;
            
            // Update all of the ID selection comboboxes.
            updateSelectIDFields();

            // Load the first beat into the panel.
            updateBeatFields();

            // First load is done.
            firstLoad = false;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads in the settings file.
        // ----------------------------------------------------------------------------------------------------
        void loadSettingsFile()
        {
            // Check to make sure that the settings file exists.
            if (File.Exists(localFolderPath + settingsFileName))
            {
                // Grab all of the lines from the settings file.
                string[] lines = File.ReadAllLines(localFolderPath + settingsFileName);

                char equalSign = Convert.ToChar("=");

                // Go through all of the lines.
                for (int i = 0; i < lines.Length; ++i)
                {
                    // Get the current line.
                    string[] current_line = lines[i].Split(equalSign);

                    // Check to make sure that it both parts.
                    if (current_line.Length > 1)
                    {
                        // Check to see if this is the setting for save prompts.
                        if (current_line[0].Contains("Prompt to save on exit"))
                        {
                            // Check to see if this setting is set to true.
                            if (current_line[1].ToLower().Contains("true"))
                            {
                                promptToSaveOnExitToolStripMenuItem.Checked = true;
                            }

                            // Otherwise, we'll set this setting to false.
                            else
                            {
                                promptToSaveOnExitToolStripMenuItem.Checked = false;
                            }
                        }

                        // Check to see if this is the setting for save prompts.
                        if (current_line[0].Contains("Case sensitive searches"))
                        {
                            // Check to see if this setting is set to true.
                            if (current_line[1].ToLower().Contains("true"))
                            {
                                caseSensitiveSearchToolStripMenuItem.Checked = true;
                            }

                            // Otherwise, we'll set this setting to false.
                            else
                            {
                                caseSensitiveSearchToolStripMenuItem.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Returns the name of the file (without the path).
        // ----------------------------------------------------------------------------------------------------
        string getFileName()
        {
            // Check if there are any backslashes in the path name (which means there's a path before it).
            if (filePathName.Contains("\\"))
            {
                // Return just the name part.
                return filePathName.Substring(filePathName.LastIndexOf("\\") + 1);
            }

            // Otherwise, there's no path before the name.
            else
            {
                // Just return the name.
                return filePathName;
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Exit the program.
        // ----------------------------------------------------------------------------------------------------
        void exitProgram()
        {
            // Check if the user wants to be prompted to save upon exiting.
            if ((promptToSaveOnExitToolStripMenuItem.Checked == true) && (panel_Selection.Enabled == true))
            {
                DialogResult saveDialogResult = MessageBox.Show("Would you like to save before closing?", "Just checking...", MessageBoxButtons.YesNo);

                // Check if the user wants to save before exiting.
                if (saveDialogResult == DialogResult.Yes)
                {
                    saveFile();
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates all of the fields with information from current beat list index.
        // ----------------------------------------------------------------------------------------------------
        void updateBeatFields()
        {
            // Clear out all of the fields.
            clearBeatFields();

            // Clear the strip menu label if it has any kind of error, message, etc.
            label_statusStrip.Text = "";

            // Load data into the General fields.
            textbox_General_ID.Text = storyBeatList[currentListIndex].id.ToString();
            textbox_General_BackgroundImage.Text = storyBeatList[currentListIndex].background_image;
            textbox_General_CharacterImage.Text = storyBeatList[currentListIndex].character_image;
            comboBox_General_BeatType.Text = storyBeatList[currentListIndex].type.ToString();
            textbox_General_MainText.Text = storyBeatList[currentListIndex].main_text;

            // Check if the beat is Narrative.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                // Enable/disable panels.
                panel_Narrative.Enabled = true;
                panel_Choice.Enabled = false;
            }

            // Check if the beat is Choice.
            else if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                // Enable/disable panels.
                panel_Choice.Enabled = true;
                panel_Narrative.Enabled = false;

                // Load Choice data.
                textbox_Choice_ChoiceID_1.Text = "1";
                textbox_Choice_ChoiceText_1.Text = ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].choice_text;
                comboBox_Choice_NextID_1.SelectedIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id);

                textbox_Choice_ChoiceID_2.Text = "2";
                textbox_Choice_ChoiceText_2.Text = ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].choice_text;
                comboBox_Choice_NextID_2.SelectedIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].next_id);

                textbox_Choice_ChoiceID_3.Text = "3";
                textbox_Choice_ChoiceText_3.Text = ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].choice_text;
                comboBox_Choice_NextID_3.SelectedIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].next_id);
            }

            // Update the previous/next beat fields.
            updatePreviousBeatField();
            updateNextBeatField();
            updateNextIDFields();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates the information in the Previous beat field section.
        // ----------------------------------------------------------------------------------------------------
        void updatePreviousBeatField()
        {
            // Clear the fields.
            textbox_Next_ID.Clear();
            textbox_Next_BackgroundImage.Clear();
            textbox_Next_CharacterImage.Clear();
            textbox_Next_BeatType.Clear();

            // Find the index of the previous beat.
            int index = findPreviousBeatIndex();

            // Check to see if a previous beat was found.
            if (index != -1)
            {
                // If the previous beat is a Narrative beat.
                if (storyBeatList[index].type == StoryBeatType.Narrative)
                {
                    // Update all of the Previous fields.
                    textbox_Previous_ID.Text = storyBeatList[index].id.ToString();
                    textbox_Previous_BackgroundImage.Text = storyBeatList[index].background_image;
                    textbox_Previous_CharacterImage.Text = storyBeatList[index].character_image;
                    textbox_Previous_BeatType.Text = storyBeatList[index].type.ToString();
                    textbox_Previous_MainText.Text = storyBeatList[index].main_text;
                }

                // If the previous beat is a Choice beat.
                else if(storyBeatList[index].type == StoryBeatType.Choice)
                {
                    int choice_index = -1;

                    // Figure out which choice has the current ID listed as its next ID.
                    for (int i = 0; i < 3; ++i)
                    {
                        // If the choice has the current beat listed as its next beat, then that's the current beat's previous beat.
                        if (((StoryBeatChoice)storyBeatList[index]).choices[i].next_id == storyBeatList[currentListIndex].id)
                        {
                            choice_index = i;
                        }
                    }

                    // Check to make sure that the choice was actually found.
                    if (choice_index != -1)
                    {
                        // Update all of the Previous fields.
                        textbox_Previous_ID.Text = storyBeatList[index].id.ToString() + "-" + (choice_index + 1).ToString();
                        textbox_Previous_BackgroundImage.Text = storyBeatList[index].background_image;
                        textbox_Previous_CharacterImage.Text = storyBeatList[index].character_image;
                        textbox_Previous_BeatType.Text = storyBeatList[index].type.ToString();
                        textbox_Previous_MainText.Text = "[" + storyBeatList[index].main_text + "]: " + ((StoryBeatChoice)storyBeatList[index]).choices[choice_index].choice_text;
                    }

                    // Wasn't able to find the choice, just display previous like Narrative.
                    else
                    {
                        // Update all of the Previous fields.
                        textbox_Previous_ID.Text = storyBeatList[index].id.ToString();
                        textbox_Previous_BackgroundImage.Text = storyBeatList[index].background_image;
                        textbox_Previous_CharacterImage.Text = storyBeatList[index].character_image;
                        textbox_Previous_BeatType.Text = storyBeatList[index].type.ToString();
                        textbox_Previous_MainText.Text = storyBeatList[index].main_text;
                    }
                }
            }

        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates the information in the Next beat field section.
        // ----------------------------------------------------------------------------------------------------
        void updateNextBeatField()
        {
            int index = -1;

            // Clear the fields.
            textbox_Next_ID.Clear();
            textbox_Next_BackgroundImage.Clear();
            textbox_Next_CharacterImage.Clear();
            textbox_Next_BeatType.Clear();

            // Check if the current beat is a Narrative one.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                index = findListIndexbyBeatID(((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id);
            }

            // Check if the current beat is a Choice one.
            else if(storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                // DECISION: It just returns the next ID from the first choice. (?)
                index = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id);
            }

            // Check if the next beat was successfully found.
            if (index != -1)
            {
                // Update all of the Next fields.
                textbox_Next_ID.Text = storyBeatList[index].id.ToString();
                textbox_Next_BackgroundImage.Text = storyBeatList[index].background_image;
                textbox_Next_CharacterImage.Text = storyBeatList[index].character_image;
                textbox_Next_BeatType.Text = storyBeatList[index].type.ToString();
                textbox_Next_MainText.Text = storyBeatList[index].main_text;
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Clears out all of the fields in all of the panels.
        // ----------------------------------------------------------------------------------------------------
        void clearBeatFields()
        {
            // Clear out all of the General fields.
            textbox_General_ID.Clear();
            textbox_General_BackgroundImage.Clear();
            textbox_General_CharacterImage.Clear();
            comboBox_General_BeatType.Text = "";
            textbox_General_MainText.Clear();

            // Clear out all of the Previous fields.
            textbox_Previous_ID.Clear();
            textbox_Previous_BackgroundImage.Clear();
            textbox_Previous_CharacterImage.Clear();
            textbox_Previous_BeatType.Clear();
            textbox_Previous_MainText.Clear();

            // Clear out all of the Next fields.
            textbox_Next_ID.Clear();
            textbox_Next_BackgroundImage.Clear();
            textbox_Next_CharacterImage.Clear();
            textbox_Next_BeatType.Clear();
            textbox_Next_MainText.Clear();

            // Clear out all of the Narrative fields.
            comboBox_Narrative_NextID.Text = "";

            // Clear out all of the Choice fields.
            textbox_Choice_ChoiceID_1.Clear();
            textbox_Choice_ChoiceText_1.Clear();
            comboBox_Choice_NextID_1.Text = "";

            textbox_Choice_ChoiceID_2.Clear();
            textbox_Choice_ChoiceText_2.Clear();
            comboBox_Choice_NextID_2.Text = "";

            textbox_Choice_ChoiceID_3.Clear();
            textbox_Choice_ChoiceText_3.Clear();
            comboBox_Choice_NextID_3.Text = "";
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Finds which beat has the current beat listed as its next ID.
        // ----------------------------------------------------------------------------------------------------
        int findPreviousBeatIndex()
        {
            // Go through all of the beats in the list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check if this beat is a Narrative one.
                if (storyBeatList[i].type == StoryBeatType.Narrative)
                {
                    // Check to see if its next beat ID matches the ID of the current beat.
                    if (((StoryBeatNarrative)storyBeatList[i]).next_id == storyBeatList[currentListIndex].id)
                    {
                        return i;
                    }
                }

                // Check if this beat is a Choice one.
                else if (storyBeatList[i].type == StoryBeatType.Choice)
                {
                    // Go through all of the choices.
                    for (int j = 0; j < ((StoryBeatChoice)storyBeatList[i]).choices.Length; ++j)
                    {
                        if (((StoryBeatChoice)storyBeatList[i]).choices[j].next_id == storyBeatList[currentListIndex].id)
                        {
                            return i;
                        }
                    }   
                }
            }

            // Unable to find a previous beat, so return -1.
            return -1;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the previous beat, based off of which beat has it listed as next.
        // ----------------------------------------------------------------------------------------------------
        void previousBeat()
        {
            // Find the index of the previous beat.
            int index = findPreviousBeatIndex();

            // Check to see if a previous beat was found.
            if (index != -1)
            {
                currentListIndex = index;
                comboBox_Selection.SelectedIndex = currentListIndex;
            }

            // Otherwise, an index wasn't found, so show an error.
            else
            {
                // Show an error in the status strip.
                label_statusStrip.Text = "ERROR: Unable to find list index for previous story beat ID.";

                // Clear out the Previous beat fields.
                textbox_Previous_ID.Clear();
                textbox_Previous_BackgroundImage.Clear();
                textbox_Previous_CharacterImage.Clear();
                textbox_Previous_BeatType.Clear();

                return;
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the next beat, based off of what the next ID is.
        // ----------------------------------------------------------------------------------------------------
        void nextBeat()
        {
            int newIndex = -1; 

            // Check if the current beat is a Narrative beat.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                // Update the current list index with whatever the list index is of the beat that's next.
                newIndex = findListIndexbyBeatID(((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id);

                // Check to make sure that an index was found.
                if (newIndex > -1)
                {
                    // Update the beat fields with the new current beat.
                    currentListIndex = newIndex;
                    comboBox_Selection.SelectedIndex = currentListIndex;
                }

            }

            // Check if the current beat is a Choice beat.
            else if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                // DECISION: Just find the next ID for whatever the first choice is. (?)
                newIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id);

                // Check to make sure that an index was found.
                if (newIndex > -1)
                {
                    // Update the beat fields with the new current beat.
                    currentListIndex = newIndex;
                    updateBeatFields();
                }
            }

            // Check to see if a next beat couldn't be found.
            if (newIndex == -1)
            {
                // Show an error in the status strip.
                label_statusStrip.Text = "ERROR: Unable to find list index for next story beat ID.";

                // Clear out the Next fields.
                textbox_Next_ID.Clear();
                textbox_Next_BackgroundImage.Clear();
                textbox_Next_CharacterImage.Clear();
                textbox_Next_BeatType.Clear();
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Finds the beat list index of the beat with the specified ID.
        // ----------------------------------------------------------------------------------------------------
        int findListIndexbyBeatID(int newID)
        {
            // Go through the whole beat list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check to see if the ID for the current beat matches.
                if (storyBeatList[i].id == newID)
                {
                    // Return the current index.
                    return i;
                }
            }

            // Return -1 as a default value, meaning that the index couldn't be found.
            return -1;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates all of the comboboxes with the IDs/information from the beat list.
        // ----------------------------------------------------------------------------------------------------
        void updateSelectIDFields()
        {
            // Clear the comboboxes first.
            for (int i = 0; i < selectIDFieldsList.Count; ++i)
            {
                selectIDFieldsList[i].Items.Clear();
            }

            // Go through all of the items in the story beat list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Add the item to each of the comboboxes.
                for (int j = 0; j < selectIDFieldsList.Count; ++j)
                {
                    selectIDFieldsList[j].Items.Add("[" + storyBeatList[i].id + "]: " + storyBeatList[i].main_text);
                }
            }

            // If we're deleting the last beat in the list, then we'll need to back up one.
            if (currentListIndex == storyBeatList.Count)
            {
                --currentListIndex;
            }

            // Check to make sure there's a current ID.
            if (currentListIndex != -1)
            {
                // Update the combobox for the currently selected ID.
                comboBox_Selection.SelectedIndex = currentListIndex;
            }

            updateNextIDFields();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates the next ID comboboxes for either Narrative or Choice.
        // ----------------------------------------------------------------------------------------------------
        void updateNextIDFields()
        {
            // Check if the beat is Narrative.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                // Find the list index for the next ID.
                int nextIndex = findListIndexbyBeatID(((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id);

                // Check to make sure a list index was actually found.
                if (nextIndex != -1)
                {
                    // Update the combobox selection.
                    comboBox_Narrative_NextID.SelectedIndex = nextIndex;
                    comboBox_Narrative_NextID.Text = comboBox_Narrative_NextID.Items[nextIndex].ToString();
                }
                else
                {
                    comboBox_Narrative_NextID.SelectedIndex = -1;
                    comboBox_Narrative_NextID.Text = "";
                }
            }

            // Check if the beat is Choice.
            else if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                int nextIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id);

                if (nextIndex != -1)
                {
                    comboBox_Choice_NextID_1.SelectedIndex = nextIndex;
                    comboBox_Choice_NextID_1.Text = comboBox_Choice_NextID_1.Items[nextIndex].ToString();
                }
                else
                {
                    comboBox_Choice_NextID_1.SelectedIndex = -1;
                    comboBox_Choice_NextID_1.Text = "";
                }

                nextIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].next_id);

                if (nextIndex != -1)
                {
                    comboBox_Choice_NextID_2.SelectedIndex = nextIndex;
                    comboBox_Choice_NextID_2.Text = comboBox_Choice_NextID_2.Items[nextIndex].ToString();
                }
                else
                {
                    comboBox_Choice_NextID_2.SelectedIndex = -1;
                    comboBox_Choice_NextID_2.Text = "";
                }

                nextIndex = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].next_id);

                if (nextIndex != -1)
                {
                    comboBox_Choice_NextID_3.SelectedIndex = nextIndex;
                    comboBox_Choice_NextID_3.Text = comboBox_Choice_NextID_3.Items[nextIndex].ToString();
                }
                else
                {
                    comboBox_Choice_NextID_3.SelectedIndex = -1;
                    comboBox_Choice_NextID_3.Text = "";
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Updates the beat list with everything in the fields.
        // ----------------------------------------------------------------------------------------------------
        void updateBeatListFromFields()
        {
            // Don't do anything here if this is the first load.
            if (firstLoad == true)
            {
                return;
            }

            // Update all of the general beat properties.
            storyBeatList[currentListIndex].background_image = textbox_General_BackgroundImage.Text;
            storyBeatList[currentListIndex].character_image = textbox_General_CharacterImage.Text;
            storyBeatList[currentListIndex].main_text = textbox_General_MainText.Text;

            // Check to see if the current beat is Narrative.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                // Check to make sure that the beat actually has a valid next ID.
                if (comboBox_Narrative_NextID.SelectedIndex > -1)
                {
                    // Update the next ID.
                    ((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id = storyBeatList[comboBox_Narrative_NextID.SelectedIndex].id;
                }
                else
                {
                    ((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id = -1;
                }
            }

            // Check to see if the current beat is Choice.
            else if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                // Update the properties for choice 1.
                ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].choice_text = textbox_Choice_ChoiceText_1.Text;

                if (comboBox_Choice_NextID_1.SelectedIndex > -1)
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id = storyBeatList[comboBox_Choice_NextID_1.SelectedIndex].id;
                }
                else
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id = -1;
                }

                // Update the properties for choice 2.
                ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].choice_text = textbox_Choice_ChoiceText_2.Text;

                if (comboBox_Choice_NextID_2.SelectedIndex > -1)
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].next_id = storyBeatList[comboBox_Choice_NextID_2.SelectedIndex].id;
                }
                else
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].next_id = -1;
                }

                // Update the properties for choice 3.
                ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].choice_text = textbox_Choice_ChoiceText_3.Text;

                if (comboBox_Choice_NextID_3.SelectedIndex > -1)
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].next_id = storyBeatList[comboBox_Choice_NextID_3.SelectedIndex].id;
                }
                else
                {
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].next_id = -1;
                }
            }

            label_statusStrip.Text = "Updated: All fields for current beat.";
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Converts a Narrative beat to a Choice beat.
        // ----------------------------------------------------------------------------------------------------
        void convertNarrativeToChoice()
        {
            // Enable/disable panels.
            panel_Choice.Enabled = true;
            panel_Narrative.Enabled = false;

            // Replace the current beat with the converted beat.
            storyBeatList[currentListIndex] = new StoryBeatChoice(((StoryBeatNarrative)storyBeatList[currentListIndex])); 

            // Update the UI.
            updateBeatFields();

            label_statusStrip.Text = "Converted: Narrative to Choice.";
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Converts a Choice beat to a Narrative beat.
        // ----------------------------------------------------------------------------------------------------
        void convertChoiceToNarrative()
        {
            // Enable/disable panels.
            panel_Narrative.Enabled = true;
            panel_Choice.Enabled = false;

            // Replace the current beat with the converted beat.
            storyBeatList[currentListIndex] = new StoryBeatNarrative(((StoryBeatChoice)storyBeatList[currentListIndex]));

            // Update the UI.
            updateBeatFields();

            label_statusStrip.Text = "Converted: Choice to Narrative.";
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Add a new beat to the selection combobox.
        // ----------------------------------------------------------------------------------------------------
        void addBeatSelection()
        {
            // Add the beat to the list, defaults to Narrative beat, ID is +1 to whatever the last ID was.
            storyBeatList.Add(new StoryBeatNarrative(storyBeatList[storyBeatList.Count - 1].id + 1));

            // Update the current list index.
            currentListIndex = storyBeatList.Count - 1;

            // Update all of the ID selection comboboxes.
            updateSelectIDFields();

            // Select the new beat. 
            comboBox_Selection.SelectedIndex = currentListIndex;

            // Update the fields.
            updateBeatFields();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Add a new beat to the Narrative next ID box.
        // ----------------------------------------------------------------------------------------------------
        void addBeatNarrativeNextID()
        {
            // Add the beat to the list, defaults to Narrative beat, ID is +1 to whatever the last ID was.
            storyBeatList.Add(new StoryBeatNarrative(storyBeatList[storyBeatList.Count - 1].id + 1));

            // Update the beat in the list.
            ((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id = storyBeatList[storyBeatList.Count - 1].id;

            // Update all of the ID selection fields.
            updateSelectIDFields();

            // Update the combobox for the Narrative next ID.
            comboBox_Narrative_NextID.SelectedIndex = storyBeatList.Count - 1;
            comboBox_Narrative_NextID.Text = comboBox_Narrative_NextID.Items[comboBox_Narrative_NextID.SelectedIndex].ToString();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Add a new beat to one for the Choice next ID boxes.
        // ----------------------------------------------------------------------------------------------------
        void addBeatChoiceNextID(int choice_number)
        {
            // Add the beat to the list, defaults to Narrative beat, ID is +1 to whatever the last ID was.
            storyBeatList.Add(new StoryBeatNarrative(storyBeatList[storyBeatList.Count - 1].id + 1));

            // Update the beat in the list.
            ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[choice_number].next_id = storyBeatList[storyBeatList.Count - 1].id;

            // Update all of the ID selection fields.
            updateSelectIDFields();

            // Update the combobox for the Choice next ID.
            if (choice_number == 0)
            {
                comboBox_Choice_NextID_1.SelectedIndex = storyBeatList.Count - 1;
                comboBox_Choice_NextID_1.Text = comboBox_Choice_NextID_1.Items[comboBox_Choice_NextID_1.SelectedIndex].ToString();
            }
            else if (choice_number == 1)
            {
                comboBox_Choice_NextID_2.SelectedIndex = storyBeatList.Count - 1;
                comboBox_Choice_NextID_2.Text = comboBox_Choice_NextID_2.Items[comboBox_Choice_NextID_2.SelectedIndex].ToString();
            }
            else if (choice_number == 2)
            {
                comboBox_Choice_NextID_3.SelectedIndex = storyBeatList.Count - 1;
                comboBox_Choice_NextID_3.Text = comboBox_Choice_NextID_3.Items[comboBox_Choice_NextID_3.SelectedIndex].ToString();
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Delete the current beat.
        // ----------------------------------------------------------------------------------------------------
        void deleteBeat()
        {
            // Don't delete the beat if there is only one.
            if (storyBeatList.Count == 1)
            {
                label_statusStrip.Text = "ERROR: Unable to delete. At least one beat is required.";
                return;
            }

            // Make sure all beats that have the deleted beat as their next beat get updated.
            removeBeatAsNext();

            // Remove the beat from the list.
            storyBeatList.RemoveAt(currentListIndex);

            // Update the UI.
            updateSelectIDFields();
            updateBeatFields();
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Goes through and ensures that no beats have the current beat listed as their next beat.
        // ----------------------------------------------------------------------------------------------------
        void removeBeatAsNext()
        {
            // Go through all of the beats in the story beat list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check if the current beat is a Narrative beat.
                if (storyBeatList[i].type == StoryBeatType.Narrative)
                {
                    // Check if the beat has the current beat's ID as its next ID.
                    if (((StoryBeatNarrative)storyBeatList[i]).next_id == storyBeatList[currentListIndex].id)
                    {
                        ((StoryBeatNarrative)storyBeatList[i]).next_id = -1;
                    }
                }

                // Check if the current beat is a Choice beat.
                else if (storyBeatList[i].type == StoryBeatType.Choice)
                {
                    // Go through all of the choices.
                    for (int j = 0; j < 3; ++j)
                    {
                        // Check if the beat has the current beat's ID as its next ID.
                        if (((StoryBeatChoice)storyBeatList[i]).choices[j].next_id == storyBeatList[currentListIndex].id)
                        {
                            ((StoryBeatChoice)storyBeatList[i]).choices[j].next_id = -1;
                        }
                    }
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Lists beats that aren't any beats' next beats.
        // ----------------------------------------------------------------------------------------------------
        void findOrphanBeats()
        {
            // Initialize the list for orphan beats.
            List<StoryBeat> orphanBeatList = new List<StoryBeat>();

            // Go through the whole list of beats.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check to see if the current beat is listed as a next ID for any other beat.
                if (beatIDHasPrevious(storyBeatList[i].id) == false)
                {
                    // Add it to the list.
                    orphanBeatList.Add(storyBeatList[i]);
                }
            }

            // Display the list.
            loadSpecifiedBeatTable(orphanBeatList, "Interview Editor - Orphan Beats");
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Lists beats that don't lead to any other beats.
        // ----------------------------------------------------------------------------------------------------
        void findDeadBeats()
        {
            // Initialize the dead beat list.
            List<StoryBeat> deadBeatList = new List<StoryBeat>();

            // Go through the whole list of beats.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check if the current beat is a Narrative beat.
                if (storyBeatList[i].type == StoryBeatType.Narrative)
                {
                    // Check if the next ID is -1.
                    if (((StoryBeatNarrative)storyBeatList[i]).next_id == -1)
                    {
                        deadBeatList.Add(storyBeatList[i]);
                    }

                    // Otherwise, the beat has a next ID listed.
                    else
                    {
                        // Check to see if that next beat actually exists.
                        if (beatIDExists(((StoryBeatNarrative)storyBeatList[i]).next_id) == false)
                        {
                            deadBeatList.Add(storyBeatList[i]);
                        }
                    }
                }

                // Check if the current beat is a Choice beat.
                else if(storyBeatList[i].type == StoryBeatType.Choice)
                {
                    // Go through all of the choices.
                    for (int j = 0; j < 3; ++j)
                    {
                        // Check if the current choice's next ID is -1.
                        if (((StoryBeatChoice)storyBeatList[i]).choices[j].next_id == -1)
                        {
                            deadBeatList.Add(storyBeatList[i]);
                        }

                        // Check if the current choice's next ID exists or not.
                        else if (beatIDExists(((StoryBeatChoice)storyBeatList[i]).choices[j].next_id) == false)
                        {
                            deadBeatList.Add(storyBeatList[i]);
                        }
                    }
                }
            }

            // Show the list.
            loadSpecifiedBeatTable(deadBeatList, "Interview Editor - Dead Beats");
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Displays a list of beats.
        // ----------------------------------------------------------------------------------------------------
        void loadSpecifiedBeatTable(List<StoryBeat> beat_list, string windowName)
        {
            // Initialize the window.
            Form_BeatTable beatTableWindow = new Form_BeatTable(windowName);
            beatTableWindow.mainEditorRef = this;

            // Load in the specified beat list to the table.
            beatTableWindow.loadBeatList(ref beat_list);

            // Show the window.
            beatTableWindow.Show();

            // Add the window to the window list.
            windowList.Add(beatTableWindow);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Checks to see if there is an existing beat for the given ID.
        // ----------------------------------------------------------------------------------------------------
        bool beatIDExists(int check_id)
        {
            // Go through the whole beat list.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check to see if the current ID matches the one we're looking for.
                if (storyBeatList[i].id == check_id)
                {
                    // Return true since we found the beat.
                    return true;
                }
            }

            // Otherwise, return false since we didn't find it.
            return false;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Determines whether or not the given beat is another beat's next beat.
        // ----------------------------------------------------------------------------------------------------
        bool beatIDHasPrevious(int check_id)
        {
            // Go through the list of beats.
            for (int i = 0; i < storyBeatList.Count; ++i)
            {
                // Check if the current beat is Narrative.
                if (storyBeatList[i].type == StoryBeatType.Narrative)
                {
                    // Check if the current beat's next beat is the given beat.
                    if (((StoryBeatNarrative)storyBeatList[i]).next_id == check_id)
                    {
                        return true;
                    }
                }

                // Check if the current beat is Choice.
                else if (storyBeatList[i].type == StoryBeatType.Choice)
                {
                    // Go through all the choices.
                    for (int j = 0; j < 3; ++j)
                    {
                        if (((StoryBeatChoice)storyBeatList[i]).choices[j].next_id == check_id)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the selected next choice as the selected choice.
        // ----------------------------------------------------------------------------------------------------
        void loadChoiceNext(int choiceIndex)
        {
            // Figure out the list index for the choice's next ID.
            int index = findListIndexbyBeatID(((StoryBeatChoice)storyBeatList[currentListIndex]).choices[choiceIndex].next_id);

            // Check to make sure that a valid index was found.
            if (index > -1)
            {
                // Update the current list index.
                currentListIndex = index;

                // Update the selection.
                comboBox_Selection.SelectedIndex = currentListIndex;
            }

            // Otherwise, just show an error in the status strip.
            else
            {
                label_statusStrip.Text = "ERROR: Unable to find list index for choice " + (choiceIndex + 1).ToString() + " next beat.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the next beat for narrative.
        // ----------------------------------------------------------------------------------------------------
        void loadNarrativeNext()
        {
            // Figure out the list index for the choice's next ID.
            int index = findListIndexbyBeatID(((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id);

            // Check to make sure that a valid index was found.
            if (index > -1)
            {
                // Update the current list index.
                currentListIndex = index;

                // Update the selection.
                comboBox_Selection.SelectedIndex = currentListIndex;
            }

            // Otherwise, just show an error in the status strip.
            else
            {
                label_statusStrip.Text = "ERROR: Unable to find list index for next beat.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the beat table form.
        // ----------------------------------------------------------------------------------------------------
        void loadCurrentBeatTable()
        {
            // Initialize the window.
            Form_BeatTable beatTableWindow = new Form_BeatTable();
            beatTableWindow.mainEditorRef = this;
            
            // Load in the story beat list data.
            beatTableWindow.loadBeatList(ref storyBeatList);
            
            // Show the window.
            beatTableWindow.Show();

            // Add the window to the window list.
            windowList.Add(beatTableWindow);
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Load the help infomation.
        // ----------------------------------------------------------------------------------------------------
        void loadHelp()
        {
            // Check to make sure that the help file exists.
            if (File.Exists(localFolderPath + helpFilename))
            {
                // Open the help file.
                System.Diagnostics.Process.Start(localFolderPath + helpFilename);
            }

            // Otherwise, the help file is missing.
            else
            {
                // Show an error in the status strip.
                label_statusStrip.Text = "ERROR: Unable to find help file: " + localFolderPath + helpFilename;
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Show the version information for the program.
        // ----------------------------------------------------------------------------------------------------
        void showVersion()
        {
            MessageBox.Show("Version: 0.1\nCreator: Alicia Yeargin");
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Opens the menu for the settings/preferences.
        // ----------------------------------------------------------------------------------------------------
        void openSettingsMenu()
        {
            // TODO.
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads a beat selected from the beat table.
        // ----------------------------------------------------------------------------------------------------
        public void loadBeatFromTable(int selectedBeatID)
        {
            int index = findListIndexbyBeatID(selectedBeatID);

            // Check to make sure that a valid index was found.
            if (index > -1)
            {
                // Update the current list index.
                currentListIndex = index;

                // Update the selection.
                comboBox_Selection.SelectedIndex = currentListIndex;
            }

            // Otherwise, just show an error in the status strip.
            else
            {
                label_statusStrip.Text = "ERROR: Unable to find list index for selected beat.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Searches for specified item in story beat text. (Case Sensitive)
        // ----------------------------------------------------------------------------------------------------
        void findTextCaseSensitive()
        {
            // Get the search text from the Find textbox.
            string searchItem = textbox_Menu_Find.Text;

            // Don't do anything if the user didn't actually input any text.
            if (searchItem == "")
            {
                return;
            }

            // Clear the text box.
            textbox_Menu_Find.Clear();

            // Initialize the list for the beats that will be found.
            List<StoryBeat> foundBeats = new List<StoryBeat>();

            // Go through all of the 
            for (int index = 0; index < storyBeatList.Count; ++index)
            {
                // Check if the main text contains the specified search item.
                if (storyBeatList[index].main_text.Contains(searchItem))
                {
                    // Add current beat to the list.
                    foundBeats.Add(storyBeatList[index]);
                }

                // Check if the current beat is a Choice beat.
                else if (storyBeatList[index].type == StoryBeatType.Choice)
                {
                    // Check if choice 1 contains the specified search item.
                    if (((StoryBeatChoice)storyBeatList[index]).choices[0].choice_text.Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, check if choice 2 contains the specified search item.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[1].choice_text.Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, cheeck if choice 3 contains the specified search item.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[2].choice_text.Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }
                }

            }

            // If at least one beat was found, then display the table.
            if (foundBeats.Count > 0)
            {
                loadSpecifiedBeatTable(foundBeats, "Interview Editor - Beats found with '" + searchItem + "'");
            }

            // Otherwise, just display status message saying that no beats could be found with the specified search item.
            else
            {
                label_statusStrip.Text = "Unable to find any story beat text containing '" + searchItem + "'.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Searches for specified item in story beat text. (Case Insensitive)
        // ----------------------------------------------------------------------------------------------------
        void findTextCaseInsensitive()
        {
            // Get the search text from the Find textbox.
            string searchItem = textbox_Menu_Find.Text.ToLower();

            // Don't do anything if the user didn't actually input any text.
            if (searchItem == "")
            {
                return;
            }

            // Clear the text box.
            textbox_Menu_Find.Clear();

            // Initialize the list for the beats that will be found.
            List<StoryBeat> foundBeats = new List<StoryBeat>();

            // Go through all of the 
            for (int index = 0; index < storyBeatList.Count; ++index)
            {
                // Check if the main text contains the specified search item.
                if (storyBeatList[index].main_text.ToLower().Contains(searchItem))
                {
                    // Add current beat to the list.
                    foundBeats.Add(storyBeatList[index]);
                }

                // Check if the current beat is a Choice beat.
                else if (storyBeatList[index].type == StoryBeatType.Choice)
                {
                    // Check if choice 1 contains the specified search item.
                    if (((StoryBeatChoice)storyBeatList[index]).choices[0].choice_text.ToLower().Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, check if choice 2 contains the specified search item.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[1].choice_text.ToLower().Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, cheeck if choice 3 contains the specified search item.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[2].choice_text.ToLower().Contains(searchItem))
                    {
                        foundBeats.Add(storyBeatList[index]);
                    }
                }

            }

            // If at least one beat was found, then display the table.
            if (foundBeats.Count > 0)
            {
                loadSpecifiedBeatTable(foundBeats, "Interview Editor - Beats found with '" + searchItem + "'");
            }

            // Otherwise, just display status message saying that no beats could be found with the specified search item.
            else
            {
                label_statusStrip.Text = "Unable to find any story beat text containing '" + searchItem + "'.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Gathers a list of all beats leading to the current beat.
        // ----------------------------------------------------------------------------------------------------
        void findAllPrevious()
        {
            // Initialize the list for the story beats.
            List<StoryBeat> previousBeats = new List<StoryBeat>();

            // Go through the whole list of beats.
            for (int index = 0; index < storyBeatList.Count; ++index)
            {
                // Check if the current beat is a Narrative beat.
                if (storyBeatList[index].type == StoryBeatType.Narrative)
                {
                    // Check if the current beat's next beat is the selected one.
                    if (((StoryBeatNarrative)storyBeatList[index]).next_id == storyBeatList[currentListIndex].id)
                    {
                        previousBeats.Add(storyBeatList[index]);
                    }
                }

                // Check if the current beat is a Choice beat.
                else if (storyBeatList[index].type == StoryBeatType.Choice)
                {
                    // Check if the next beat for choice 1 is the selected beat.
                    if (((StoryBeatChoice)storyBeatList[index]).choices[0].next_id == storyBeatList[currentListIndex].id)
                    {
                        previousBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, check if the next beat for choice 2 is the selected beat.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[1].next_id == storyBeatList[currentListIndex].id)
                    {
                        previousBeats.Add(storyBeatList[index]);
                    }

                    // Otherwise, check if the next beat for choice 3 is the selected beat.
                    else if (((StoryBeatChoice)storyBeatList[index]).choices[2].next_id == storyBeatList[currentListIndex].id)
                    {
                        previousBeats.Add(storyBeatList[index]);
                    }
                }
            }

            // Check if any previous beats were actually found.
            if (previousBeats.Count > 0)
            {
                loadSpecifiedBeatTable(previousBeats, "Interview Editor - Previous Beats (" + storyBeatList[currentListIndex].id.ToString() + ")");
            }

            // Otherwise, no previous beats were found, so just show a message on the status strip.
            else
            {
                label_statusStrip.Text = "Unable to find any previous beats.";
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ========================================================================================================================================================================================================
        // EVENTS
        // ========================================================================================================================================================================================================

        // Menu Strip: New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        // Menu Strip: Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        // Menu Strip: Save as 
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveNewFile();
        }

        // Menu Strip: Load
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFileDialog();
        }

        // Menu Strip: Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // General: Previous
        private void button_General_Previous_Click(object sender, EventArgs e)
        {
            previousBeat();
        }

        // General: Next
        private void button_General_Next_Click(object sender, EventArgs e)
        {
            nextBeat();
        }

        // Combobox: Beat Type
        private void comboBox_General_BeatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if Narrative beat is being changed to a Choice beat.
            if ((storyBeatList[currentListIndex].type == StoryBeatType.Narrative) && (comboBox_General_BeatType.Text == "Choice"))
            {
                convertNarrativeToChoice();
            }

            // Check if Choice beat is being changed to a Narrative beat.
            else if ((storyBeatList[currentListIndex].type == StoryBeatType.Choice) && (comboBox_General_BeatType.Text == "Narrative"))
            {
                convertChoiceToNarrative();
            }
        }

        // Menu Strip: Previous ID
        private void previousIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousBeat();
        }

        // Menu Strip: Next ID
        private void nextIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextBeat();
        }

        // Texbox: Leave Background Image
        private void textbox_General_BackgroundImage_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            storyBeatList[currentListIndex].background_image = textbox_General_BackgroundImage.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Background Image.";
        }

        // Texbox: Leave Character Image
        private void textbox_General_CharacterImage_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            storyBeatList[currentListIndex].character_image = textbox_General_CharacterImage.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Character Image.";
        }

        // Texbox: Leave Main Text.
        private void textbox_General_MainText_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            storyBeatList[currentListIndex].main_text = textbox_General_MainText.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Main Text.";
        }

        // ComboBox: Leave Next ID.
        private void comboBox_Narrative_NextID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check to make sure that the current beat is a narrative beat.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Narrative)
            {
                if ((comboBox_Narrative_NextID.Text != "") && (((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id != storyBeatList[comboBox_Narrative_NextID.SelectedIndex].id))
                {
                    // Write data to list.
                    ((StoryBeatNarrative)storyBeatList[currentListIndex]).next_id = storyBeatList[comboBox_Narrative_NextID.SelectedIndex].id;
                    updateNextBeatField();
                    label_statusStrip.Text = "Updated: Next ID.";
                }
            }
        }

        // ComboBox: Change Next ID Choice 1.
        private void comboBox_Choice_NextID_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check to make sure that the current beat is a narrative beat.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                if (comboBox_Choice_NextID_1.Text != "")
                {
                    // Write data to list.
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].next_id = storyBeatList[comboBox_Choice_NextID_1.SelectedIndex].id;
                    updateNextBeatField();
                    label_statusStrip.Text = "Updated: Choice 1 Next ID.";
                }
            }
        }

        // ComboBox: Change Next ID Choice 2.
        private void comboBox_Choice_NextID_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check to make sure that the current beat is a narrative beat.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                if (comboBox_Choice_NextID_2.Text != "")
                {
                    // Write data to list.
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].next_id = storyBeatList[comboBox_Choice_NextID_2.SelectedIndex].id;
                    updateNextBeatField();
                    label_statusStrip.Text = "Updated: Choice 2 Next ID.";
                }
            }
        }

        // ComboBox: Change Next ID Choice 3.
        private void comboBox_Choice_NextID_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check to make sure that the current beat is a narrative beat.
            if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
            {
                if (comboBox_Choice_NextID_3.Text != "")
                {
                    // Write data to list.
                    ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].next_id = storyBeatList[comboBox_Choice_NextID_3.SelectedIndex].id;
                    updateNextBeatField();
                    label_statusStrip.Text = "Updated: Choice 3 Next ID.";
                }
            }
        }

        // Texbox: Leave Choice 1 Text.
        private void textbox_Choice_ChoiceText_1_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[0].choice_text = textbox_Choice_ChoiceText_1.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Choice 1 Text.";
        }

        // Texbox: Leave Choice 2 Text.
        private void textbox_Choice_ChoiceText_2_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[1].choice_text = textbox_Choice_ChoiceText_2.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Choice 2 Text.";
        }

        // Texbox: Leave Choice 3 Text.
        private void textbox_Choice_ChoiceText_3_Leave(object sender, EventArgs e)
        {
            // Write data to list.
            ((StoryBeatChoice)storyBeatList[currentListIndex]).choices[2].choice_text = textbox_Choice_ChoiceText_3.Text;
            updateSelectIDFields();
            label_statusStrip.Text = "Updated: Choice 3 Text.";
        }

        // ComboBox: ID selection.
        private void comboBox_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentListIndex = comboBox_Selection.SelectedIndex;
            updateBeatFields();
            updateNextIDFields();
        }

        // Form Closing
        private void Form1_FormClosing(object sender, EventArgs e)
        {
            exitProgram();
        }

        // Plus Button
        private void button_Selection_Add_Click(object sender, EventArgs e)
        {
            addBeatSelection();
        }

        // Minus Button
        private void button_Selection_Delete_Click(object sender, EventArgs e)
        {
            deleteBeat();
        }

        // Button Click Narrative New ID
        private void button_Narrative_NewID_Click(object sender, EventArgs e)
        {
            addBeatNarrativeNextID();
        }

        // Button Click Choice 1 New ID
        private void button_Choice_NewID_1_Click(object sender, EventArgs e)
        {
            addBeatChoiceNextID(0);
        }

        // Button Click Choice 2 New ID
        private void button_Choice_NewID_2_Click(object sender, EventArgs e)
        {
            addBeatChoiceNextID(1);
        }

        // Button Click Choice 3 New ID
        private void button_Choice_NewID_3_Click(object sender, EventArgs e)
        {
            addBeatChoiceNextID(2);
        }

        // Menu: Edit: Find orphan beats...
        private void findOrphanBeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findOrphanBeats();
        }

        // Menu: Edit: Find dead beats...
        private void findDeadBeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findDeadBeats();
        }

        // Menu: Changing the Save Prompt setting.
        private void changeSettingStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSettingsFile();
        }

        // Button: Click choice 1 'Load' button.
        private void button_Choice_LoadID_1_Click(object sender, EventArgs e)
        {
            loadChoiceNext(0);
        }

        // Button: Click choice 2 'Load' button.
        private void button_Choice_LoadID_2_Click(object sender, EventArgs e)
        {
            loadChoiceNext(1);
        }

        // Button: Click choice 3 'Load' button.
        private void button_Choice_LoadID_3_Click(object sender, EventArgs e)
        {
            loadChoiceNext(2);
        }

        // Button: Click the Narrative next 'Load' button.
        private void button_Narrative_Load_Click(object sender, EventArgs e)
        {
            loadNarrativeNext();
        }

        // Menu/keyboard shortcut: Load the next beat for choice 1.
        private void loadChoiceNext1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check to make sure current list is valid (aka that we did load some beats).
            if (currentListIndex > -1)
            {
                // Check to make sure that the currently selected beat is a Choice beat.
                if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
                {
                    loadChoiceNext(0);
                }
            }
        }

        // Menu/keyboard shortcut: Load the next beat for choice 2
        private void loadChoiceNext2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check to make sure current list is valid (aka that we did load some beats).
            if (currentListIndex > -1)
            {
                // Check to make sure that the currently selected beat is a Choice beat.
                if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
                {
                    loadChoiceNext(1);
                }
            }
        }

        // Menu/keyboard shortcut: Load the next beat for choice 3
        private void loadChoiceNext3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check to make sure current list is valid (aka that we did load some beats).
            if (currentListIndex > -1)
            {
                // Check to make sure that the currently selected beat is a Choice beat.
                if (storyBeatList[currentListIndex].type == StoryBeatType.Choice)
                {
                    loadChoiceNext(2);
                }
            }
        }

        // Button: Choice 'New All'.
        private void button_Choice_NewID_All_Click(object sender, EventArgs e)
        {
            addBeatChoiceNextID(0);
            addBeatChoiceNextID(1);
            addBeatChoiceNextID(2);
        }

        // Menu About > Help
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadHelp();
        }

        // Menu About > Version
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showVersion();
        }

        // Menu Edit > Settings
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSettingsMenu();
        }

        // Menu View > Beat Table
        private void beatTableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            loadCurrentBeatTable();
        }

        // Pressing the Enter key in text boxes.
        private void textboxHitEnterKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Check that the enter key was pressed.
            if (e.KeyCode == Keys.Enter)
            {
                updateBeatListFromFields();
            }
        }

        // Pressing the Enter key in one of the WPF text boxes.
        private void UITextboxHitEnterKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Check that the enter key was pressed.
            if (e.Key == Key.Enter)
            {
                updateBeatListFromFields();
            }
        }

        // Click 'Find' button.
        private void button_Menu_Find_Click(object sender, EventArgs e)
        {
            // Check whether the user has checked case sensitive for searches.
            if (caseSensitiveSearchToolStripMenuItem.Checked == true)
            {
                findTextCaseSensitive();
            }

            // The user has case sensitive searches disabled.
            else
            {
                findTextCaseInsensitive();
            }
        }

        // Hit Enter key in the Find text box.
        private void searchboxHitEnterKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Check if the enter key was pressed.
            if (e.KeyCode == Keys.Enter)
            {
                // Check whether the user has checked case sensitive for searches.
                if (caseSensitiveSearchToolStripMenuItem.Checked == true)
                {
                    findTextCaseSensitive();
                }

                // The user has case sensitive searches disabled.
                else
                {
                    findTextCaseInsensitive();
                }
            }
        }

        // Button 'Find All' previous
        private void button1_Click(object sender, EventArgs e)
        {
            findAllPrevious();
        }

        // Menu View > Find previous beats...
        private void findPreviousBeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findAllPrevious();
        }
    }
}
