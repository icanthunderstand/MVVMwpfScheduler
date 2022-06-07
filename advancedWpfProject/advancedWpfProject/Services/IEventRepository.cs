using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advancedWpfProject.MVVM.Model;

namespace advancedWpfProject.Services
{
    public interface IEventRepository
    {
        int AddEvent(Event ev);
        List<Event> GetEvents();
        List<Event> GetEvents(string LoginId);
        int DeleteEvent(int id);
        bool UpdateEvent(Event ev);
        int GetNextEventId();
    }
}
