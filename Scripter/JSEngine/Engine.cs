using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ClearScript.V8;
using Scripter;
using Microsoft.ClearScript;
using System.Windows.Forms;


/*
 * To do:
 * 
 * More classes exposed and more examples
 * 
 * 
 * 
 * 
 */

namespace Engine_Work
{
    public static class Startup
    {
        private static V8ScriptEngine Engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDebugging);
        public static void Init()
        {

            Engine.AddHostType("Console", typeof(Safe));
            Engine.AddHostType("Dialogs", typeof(Dialogs));
            //Engine.AddHostType("MDI", typeof(MDIParent1));

        }

        public static void Run(string Script)
        {


            try
            {

                Engine.Execute(Script);
                Safe.output = new Ouptut();
                Safe.isUsed = false;
            }
            catch (ScriptEngineException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



    public static class Safe
    {
        public static bool isUsed = false;
        public static Ouptut output = new Ouptut();
        public static void Init()
        {
            output.Show();
            isUsed = true;
        }


        //meant to be used funcs
        public static void Log(string input)
        {
            if (!isUsed)
            {
                Init();
            }
            output.Addline(input);
        }

        public static void ClearText()
        {
            if (!isUsed)
            {
                Init();
            }
            output.DeleteText();
        }



        public static void Close()
        {
            if (!isUsed)
            {
                Init();
            }
            output.Close();
        }
    }

    public static class Dialogs
    {
        public static void ErrorBoxShow(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void InfoBoxShow(string Message)
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void WarnBoxShow(string Message)
        {
            MessageBox.Show(Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool QuestionBoxShow(string Message)
        {
            if (MessageBox.Show(Message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                return true;

            }
            else
            {

                return false;
            }
        }

        public static bool ErrorBoxYesNo(string Message)
        {
            if (MessageBox.Show(Message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {

                return true;

            }
            else
            {

                return false;
            }
        }
    }
}


