using BK_PCR.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public partial class User
    {
		public User()
		{ }
		#region  BasicMethod

		/// <summary>
		/// 获得用户数据列表
		/// </summary>
		public DataSet GetListUser()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT ID,username,is_Disable,is_Locking,CreateTime,LastLogonTime");
			strSql.Append(" FROM department ");
			return DbHelperOleDb.Query(strSql.ToString());
		}
	}

}
#endregion