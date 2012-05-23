using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication.Frames.Incoming;

namespace Configuration.NavigationCommands
{
    public partial class Block : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public Block(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.opcode = NavigationInstruction.navigation_command.BLOCK;
            ni.StringToArgument(_tbName.Text.Substring(0, Math.Min(8, _tbName.Text.Length)));
            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            try
            {
                _tbName.Text = ni.GetStringArgument();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
