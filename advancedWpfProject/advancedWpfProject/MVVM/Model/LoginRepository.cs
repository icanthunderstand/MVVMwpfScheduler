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
    public class LoginRepository : ILoginRepository
    {
        private SqlConnection db;

        public LoginRepository()
        {
            db = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database = EVENTDB; Trusted_Connection=True;");
        }

        public bool Login(string loginId,string password)
        {
            var par = new DynamicParameters();

            par.Add("@LoginId", value: loginId, dbType: DbType.String);
            par.Add("@Password", value: password, dbType: DbType.String);

            return db.Query<int>("LOGIN_LOGIN_SP", par, commandType: CommandType.StoredProcedure).SingleOrDefault() == 1;            
        }

        public string GetNickName(string loginId)
        {
            if (loginId.Equals(string.Empty) |
                loginId.Equals("Guest"))
            {
                return string.Empty;
            }
            string sql = @"SELECT NickName FROM LOGIN_TB WHERE LoginId = @loginId";
            var par = new DynamicParameters();
            par.Add("@LoginId", value: loginId, dbType: DbType.String);

            return db.Query<string>(sql,par,commandType:CommandType.Text).SingleOrDefault();
        }

        public bool RegisterAccount(string loginId, string password,string nickName)
        {
            var par = new DynamicParameters();

            par.Add("@LoginId", value: loginId, dbType: DbType.String);
            par.Add("@Password", value: password, dbType: DbType.String);
            par.Add("@NickName", value: nickName, dbType: DbType.String);

            return db.Query<int>("LOGIN_INSERT_SP", par,commandType: CommandType.StoredProcedure).SingleOrDefault() == 1;
        }

        /*
        public int DeleteAccount(string loginId, string password)
        {
            var par = new DynamicParameters();

            par.Add("@LoginId", value: loginId, dbType: DbType.String);
            par.Add("@Password", value: password, dbType: DbType.String);
            par.Add("@NickName", value: nickName, dbType: DbType.String);

            db.Execute("LOGIN_DELETE_SP", par);

            return 1;
        }
        */



    }
}
