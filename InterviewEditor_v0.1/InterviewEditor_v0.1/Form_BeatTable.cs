// ========================================================================================================================================================================================================
// Project: Interview Editor
// File: Form_BeatTable
// Creator: Alicia Yeargin
// ========================================================================================================================================================================================================

using System.Collections.Generic;
using System.Windows.Forms;

namespace InterviewEditor_v0._1
{
    public partial class Form_BeatTable : Form
    {
        // Members.
        public Form mainEditorRef;      // Reference the the main editor window.

        // ----------------------------------------------------------------------------------------------------
        // INITIALIZATION - Default
        // ----------------------------------------------------------------------------------------------------
        public Form_BeatTable()
        {
            // Initialize the form.
            InitializeComponent();

            // Add the double click event to the cells.
            dataGridView_beatTable.CellDoubleClick += dataGridView_Cell_DoubleClick;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // INITIALIZATION - With Custom Window Name
        // ----------------------------------------------------------------------------------------------------
        public Form_BeatTable(string newWindowName)
        {
            // Initialize the form.
            InitializeComponent();

            // Add the double click event to the cells.
            dataGridView_beatTable.CellDoubleClick += dataGridView_Cell_DoubleClick;

            // Set the window name.
            Text = newWindowName;
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // FUNCTION: Loads in the story beat list data to the data grid view.
        // ----------------------------------------------------------------------------------------------------
        public void loadBeatList(ref List<Form_MainEditor.StoryBeat> listData)
        {
            // Go through the whole list.
            for (int index = 0; index < listData.Count; ++index)
            {
                int currentRow = dataGridView_beatTable.Rows.Add();

                dataGridView_beatTable.Rows[currentRow].Cells[0].Value = listData[index].id;
                dataGridView_beatTable.Rows[currentRow].Cells[1].Value = listData[index].type;
                dataGridView_beatTable.Rows[currentRow].Cells[2].Value = listData[index].main_text;
                dataGridView_beatTable.Rows[currentRow].Cells[3].Value = listData[index].background_image;
                dataGridView_beatTable.Rows[currentRow].Cells[4].Value = listData[index].character_image;

                // Check if the current beat is a Narrative beat.
                if (listData[index].type == Form_MainEditor.StoryBeatType.Narrative)
                {
                    dataGridView_beatTable.Rows[currentRow].Cells[5].Value = ((Form_MainEditor.StoryBeatNarrative)listData[index]).next_id;
                }

                // Check if the current beat is a Choice beat.
                else if (listData[index].type == Form_MainEditor.StoryBeatType.Choice)
                {
                    dataGridView_beatTable.Rows[currentRow].Cells[6].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[0].choice_text;
                    dataGridView_beatTable.Rows[currentRow].Cells[7].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[0].next_id;
                    dataGridView_beatTable.Rows[currentRow].Cells[8].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[1].choice_text;
                    dataGridView_beatTable.Rows[currentRow].Cells[9].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[1].next_id;
                    dataGridView_beatTable.Rows[currentRow].Cells[10].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[2].choice_text;
                    dataGridView_beatTable.Rows[currentRow].Cells[11].Value = ((Form_MainEditor.StoryBeatChoice)listData[index]).choices[2].next_id;
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------------------
        // Function: Loads the specified cell (row, column) into the main editor.
        // ----------------------------------------------------------------------------------------------------
        void loadSelectedCellIntoEditor(int row, int column)
        {
            // Load the beat into the editor.
            ((Form_MainEditor)mainEditorRef).loadBeatFromTable(System.Convert.ToInt32(dataGridView_beatTable.Rows[row].Cells[0].Value));

            // Set the focus on the editor.
            mainEditorRef.Focus();
        }
        // ----------------------------------------------------------------------------------------------------

        // Event: Double click a cell.
        private void dataGridView_Cell_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loadSelectedCellIntoEditor(e.RowIndex, e.ColumnIndex);
        }
    }
}
