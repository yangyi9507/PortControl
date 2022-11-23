using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using PortControlDemo.Model;
namespace PortControlDemo.BLL
{
	public partial class DeepHoleBase
	{

		private readonly PortControlDemo.DAL.DeepHoleBase dal = new PortControlDemo.DAL.DeepHoleBase();
		public DeepHoleBase()
		{ }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{
			return dal.Exists(HOLEID, HOLENAME, HOLEXY, DEEPXY);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(PortControlDemo.Model.DeepHoleBase model)
		{
			dal.Add(model);

		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(PortControlDemo.Model.DeepHoleBase model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{

			return dal.Delete(HOLEID, HOLENAME, HOLEXY, DEEPXY);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public PortControlDemo.Model.DeepHoleBase GetModel(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{

			return dal.GetModel(HOLEID, HOLENAME, HOLEXY, DEEPXY);
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return dal.GetList(Top, strWhere, filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<PortControlDemo.Model.DeepHoleBase> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<PortControlDemo.Model.DeepHoleBase> DataTableToList(DataTable dt)
		{
			List<PortControlDemo.Model.DeepHoleBase> modelList = new List<PortControlDemo.Model.DeepHoleBase>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				PortControlDemo.Model.DeepHoleBase model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new PortControlDemo.Model.DeepHoleBase();
					if (dt.Rows[n]["HOLEID"].ToString() != "")
					{
						model.HOLEID = int.Parse(dt.Rows[n]["HOLEID"].ToString());
					}
					model.HOLENAME = dt.Rows[n]["HOLENAME"].ToString();
					model.HOLEXY = dt.Rows[n]["HOLEXY"].ToString();
					model.DEEPXY = dt.Rows[n]["DEEPXY"].ToString();


					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
		#endregion

	}
}