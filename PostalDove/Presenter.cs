using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalDove
{
    public interface IView
    {
        event EventHandler<EventArgs> RefreshListView; //обновить список
        event EventHandler<EventArgs> ClickAbout;      //посмотреть информацию "О программе"
        event EventHandler<EventArgs> ShowSettings;    //посмотреть настройки
        event EventHandler<EventArgs> DoTestSending;   //сделать тестовую отправку
        event EventHandler<EventArgs> ExpandForm;      //развернуть форму по клику
        event EventHandler<EventArgs> DoMainSending;   //произвести основную рассылку
    }

    class Presenter
    {
        BaseModel _model = new BaseModel();
        IView _view;

        public Presenter(IView iview)
        {
            _view = iview;
            _view.DoMainSending += new EventHandler<EventArgs>(_view_DoMainSending);
            
        }

        private void _view_DoMainSending(object sender, EventArgs e)
        {
            
        }
    }
}
