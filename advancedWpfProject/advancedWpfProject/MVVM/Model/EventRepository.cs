using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using advancedWpfProject.Services;

namespace advancedWpfProject.MVVM.Model
{
    public class EventRepository : IEventRepository
    {

        private SqlConnection db;

        public EventRepository()
        {
            db = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database = EVENTDB; Trusted_Connection=True;");            
        }

        public int AddEvent(Event ev)
        {
            var par = new DynamicParameters();

            par.Add("@Id", value: ev.Id, dbType: System.Data.DbType.Int32);
            par.Add("@Title", value: ev.Title, dbType: System.Data.DbType.String);
            par.Add("@Location", value: ev.Location, dbType: System.Data.DbType.String);
            par.Add("@Description", value: ev.Description, dbType: System.Data.DbType.String);
            par.Add("@StartTime", value: ev.StartTime, dbType: System.Data.DbType.DateTime);
            par.Add("@EndTime", value: ev.EndTime, dbType: System.Data.DbType.DateTime);
            par.Add("@Foreground", value: ev.Foreground, dbType: System.Data.DbType.String);
            par.Add("@Background", value: ev.Background, dbType: System.Data.DbType.String);
            par.Add("@LoginId", value: ev.LoginId, dbType: System.Data.DbType.String);

            return this.db.Execute("EVENT_INSERT_SP", par, commandType: CommandType.StoredProcedure);            
        }

        public List<Event> GetEvents()
        {
            string sql = @"SELECT * FROM EVENT_TB ORDER BY Id Asc";

            return this.db.Query<Event>(sql).ToList();
        }

        public List<Event> GetEvents(string LoginId)
        {
            var par = new DynamicParameters();
            par.Add("@LoginId", value: LoginId, dbType: System.Data.DbType.String);

            return this.db.Query<Event>("EVENT_SELECT_SP", param: par, commandType: CommandType.StoredProcedure).ToList();
        }

        public int DeleteEvent(int id)
        {
            var par = new DynamicParameters();

            par.Add("@Id", value: id, dbType: System.Data.DbType.Int32);

            return this.db.Execute("EVENT_DELETE_SP", par, commandType: CommandType.StoredProcedure);
        }

        public bool UpdateEvent(Event ev)
        {
            var par = new DynamicParameters();

            par.Add("@Id", value: ev.Id, dbType: System.Data.DbType.Int32);
            par.Add("@Title", value: ev.Title, dbType: System.Data.DbType.String);
            par.Add("@Location", value: ev.Location, dbType: System.Data.DbType.String);
            par.Add("@Description", value: ev.Description, dbType: System.Data.DbType.String);
            par.Add("@StartTime", value: ev.StartTime, dbType: System.Data.DbType.DateTime);
            par.Add("@EndTime", value: ev.EndTime, dbType: System.Data.DbType.DateTime);
            par.Add("@Foreground", value: ev.Foreground, dbType: System.Data.DbType.String);
            par.Add("@Background", value: ev.Background, dbType: System.Data.DbType.String);
            par.Add("@LoginId", value: ev.LoginId, dbType: System.Data.DbType.String);            

            return this.db.Query<int>("EVENT_UPDATE_SP", par, commandType: CommandType.StoredProcedure).SingleOrDefault() == 1;
        }

        public int GetNextEventId()
        {
            return this.db.Query<int>("EVENT_GETID_SP", commandType: CommandType.StoredProcedure).SingleOrDefault() + 1;
        }
        


    }
}
