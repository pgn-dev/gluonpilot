using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Configuration
{
    public partial class PidControl : UserControl
    {
        public event EventHandler IsChanged;

        [NonSerialized]
        private PidModel _model = new PidModel();

        public double P
        {
            get { return _tb_P.DoubleValue; }
            set { _tb_P.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }
        public double I
        {
            get { return _tb_I.DoubleValue; }
            set { _tb_I.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }
        public double D
        {
            get { return _tb_D.DoubleValue; }
            set { _tb_D.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }
        public double Imin
        {
            get { return _tb_Imin.DoubleValue; }
            set { _tb_Imin.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }
        public double Imax
        {
            get { return _tb_Imax.DoubleValue; }
            set { _tb_Imax.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }
        public double Dmin
        {
            get { return _tb_Dmin.DoubleValue; }
            set { _tb_Dmin.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat); }
        }

        public PidModel GetModel()
        {
            return _model;
        }

        public void SetModel(PidModel m)
        {
            if (m == null)
                return;
            P = m.P;
            I = m.I;
            D = m.D;
            Imin = m.IMin;
            Imax = m.IMax;
            Dmin = m.DMin;
        }

        public PidControl()
        {
            InitializeComponent();
            IsChanged += new EventHandler(PidControl_IsChanged);
        }

        void PidControl_IsChanged(object sender, EventArgs e)
        {
            _model.P = P;
            _model.I = I;
            _model.D = D;
            _model.IMin = Imin;
            _model.IMax = Imax;
            _model.DMin = Dmin;
        }

        private void _tb_P_TextChanged(object sender, EventArgs e)
        {
            _model.P = P;
            IsChanged(this, null);
        }

        private void _tb_I_TextChanged(object sender, EventArgs e)
        {
            _model.I = I;
            IsChanged(this, null);
        }

        private void _tb_D_TextChanged(object sender, EventArgs e)
        {
            _model.D = D;
            IsChanged(this, null);
        }

        private void _tb_Imin_TextChanged(object sender, EventArgs e)
        {
            _model.IMin = Imin;
            IsChanged(this, null);
        }

        private void _tb_Imax_TextChanged(object sender, EventArgs e)
        {
            _model.IMax = Imax;
            IsChanged(this, null);
        }

        private void _tb_Dmin_TextChanged(object sender, EventArgs e)
        {
            _model.DMin = Dmin;
            IsChanged(this, null);
        }
    }
}
