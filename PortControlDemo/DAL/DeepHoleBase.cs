using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using PortControlDemo.DBUtility;
using System.Data.OleDb;

namespace PortControlDemo.DAL
{
	public partial class DeepHoleBase
	{

		public bool Exists(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from DeepHoleBase");
			strSql.Append(" where ");
			strSql.Append(" HOLEID = @HOLEID and  ");
			strSql.Append(" HOLENAME = @HOLENAME and  ");
			strSql.Append(" HOLEXY = @HOLEXY and  ");
			strSql.Append(" DEEPXY = @DEEPXY  ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@HOLEID", OleDbType.Integer,4),
					new OleDbParameter("@HOLENAME", OleDbType.VarChar,255),
					new OleDbParameter("@HOLEXY", OleDbType.VarChar,255),
					new OleDbParameter("@DEEPXY", OleDbType.VarChar,255)            };
			parameters[0].Value = HOLEID;
			parameters[1].Value = HOLENAME;
			parameters[2].Value = HOLEXY;
			parameters[3].Value = DEEPXY;

			return DbHelperOleDb.Exists(strSql.ToString(), parameters);
		}



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(PortControlDemo.Model.DeepHoleBase model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into DeepHoleBase(");
			strSql.Append("HOLEID,HOLENAME,HOLEXY,DEEPXY");
			strSql.Append(") values (");
			strSql.Append("@HOLEID,@HOLENAME,@HOLEXY,@DEEPXY");
			strSql.Append(") ");

			OleDbParameter[] parameters = {
						new OleDbParameter("@HOLEID", OleDbType.Integer,4) ,
						new OleDbParameter("@HOLENAME", OleDbType.VarChar,255) ,
						new OleDbParameter("@HOLEXY", OleDbType.VarChar,255) ,
						new OleDbParameter("@DEEPXY", OleDbType.VarChar,255)

			};

			parameters[0].Value = model.HOLEID;
			parameters[1].Value = model.HOLENAME;
			parameters[2].Value = model.HOLEXY;
			parameters[3].Value = model.DEEPXY;
			DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);

		}


		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(PortControlDemo.Model.DeepHoleBase model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update DeepHoleBase set ");
			strSql.Append(" DEEPXY = @DEEPXY  ");
			strSql.Append(" where HOLEID=@HOLEID");

			OleDbParameter[] parameters = {
						new OleDbParameter("@DEEPXY", OleDbType.VarChar,255),
						new OleDbParameter("@HOLEID", OleDbType.Integer,4)

			};
			parameters[0].Value = model.DEEPXY;
			parameters[1].Value = model.HOLEID;
			int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from DeepHoleBase ");
			strSql.Append(" where HOLEID=@HOLEID and HOLENAME=@HOLENAME and HOLEXY=@HOLEXY and DEEPXY=@DEEPXY ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@HOLEID", OleDbType.Integer,4),
					new OleDbParameter("@HOLENAME", OleDbType.VarChar,255),
					new OleDbParameter("@HOLEXY", OleDbType.VarChar,255),
					new OleDbParameter("@DEEPXY", OleDbType.VarChar,255)            };
			parameters[0].Value = HOLEID;
			parameters[1].Value = HOLENAME;
			parameters[2].Value = HOLEXY;
			parameters[3].Value = DEEPXY;


			int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public PortControlDemo.Model.DeepHoleBase GetModel(int HOLEID, string HOLENAME, string HOLEXY, string DEEPXY)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select HOLEID, HOLENAME, HOLEXY, DEEPXY  ");
			strSql.Append("  from DeepHoleBase ");
			strSql.Append(" where HOLEID=@HOLEID and HOLENAME=@HOLENAME and HOLEXY=@HOLEXY and DEEPXY=@DEEPXY ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@HOLEID", OleDbType.Integer,4),
					new OleDbParameter("@HOLENAME", OleDbType.VarChar,255),
					new OleDbParameter("@HOLEXY", OleDbType.VarChar,255),
					new OleDbParameter("@DEEPXY", OleDbType.VarChar,255)            };
			parameters[0].Value = HOLEID;
			parameters[1].Value = HOLENAME;
			parameters[2].Value = HOLEXY;
			parameters[3].Value = DEEPXY;


			PortControlDemo.Model.DeepHoleBase model = new PortControlDemo.Model.DeepHoleBase();
			DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["HOLEID"].ToString() != "")
				{
					model.HOLEID = int.Parse(ds.Tables[0].Rows[0]["HOLEID"].ToString());
				}
				model.HOLENAME = ds.Tables[0].Rows[0]["HOLENAME"].ToString();
				model.HOLEXY = ds.Tables[0].Rows[0]["HOLEXY"].ToString();
				model.DEEPXY = ds.Tables[0].Rows[0]["DEEPXY"].ToString();

				return model;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DeepHoleBase ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM DeepHoleBase ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}


	}
}

