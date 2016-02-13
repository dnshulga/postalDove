using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalDove
{
    class MainPresenter
    {
        readonly IMailing _model;
        readonly IMainForm _view;

        public MainPresenter(IMailing model, IMainForm view)
        {
            _model = model;
            _view = view;


            _view.mainSendingClick += new EventHandler(_view_mainSendingClick);
            _view.aboutStripClick += new EventHandler(_view_aboutStripClick);
        }

        private void _view_aboutStripClick(object sender, EventArgs e)
        {
            _model.showAbout();
        }

        private void _view_mainSendingClick(object sender, EventArgs e)
        {
            _model.sendMail(_view.Subject, _view.Body, null);
        }

    }
}
