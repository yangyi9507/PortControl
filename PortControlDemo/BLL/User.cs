using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	/// <summary>
	/// AllTestItem
	/// </summary>
	public partial class UserBll
	{
		private readonly BK_PCR.DAL.UserBll dal = new BK_PCR.DAL.UserBll();
		public UserBll()
		{ }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetListUser()
		{
			return dal.GetListUser();
		}
	}
}
