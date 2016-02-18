using System;

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
            _view.testingSendingStrip += new EventHandler(_view_testingSendingStrip);
            _view.getInfoLoad += new EventHandler(_view_getInfoLoad);
            _view.settingsStripClick += new EventHandler(_view_settingsStripClick);
        }

        private void _view_settingsStripClick(object sender, EventArgs e)
        {
            _model.showSettings();
        }

        private void _view_getInfoLoad(object sender, EventArgs e)
        {
            _model.getInfo();
        }

        private void _view_testingSendingStrip(object sender, EventArgs e)
        {
            MessageMembers mb = new MessageMembers(_view.Subject, _view.Body, null, _view.actBtnTest);
            _model.threadOfTestSending(mb);
        }

        private void _view_aboutStripClick(object sender, EventArgs e)
        {
            _model.showAbout();
        }

        private void _view_mainSendingClick(object sender, EventArgs e)
        {
            MessageMembers mb = new MessageMembers(_view.Subject, _view.Body, null, null);
            _model.threadOfMainSending(mb);
        }
    }
}
