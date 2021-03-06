﻿using SqlSugarDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDal
{
    public class GroupRepository : IGroupRepository
    {
        public long Add(GroupInfo entity, bool ignorePk = true)
        {
            if (ignorePk)
                return DataAccessProxy.Add(entity, x => new { x.Id });
            else
                return DataAccessProxy.Add(entity);
        }

        public long Add(List<GroupInfo> entitys, bool ignorePk = true)
        {
            return DataAccessProxy.Add(entitys);
        }

        public int Delete(int[] primaryKeys, bool isLogic)
        {
            return DataAccessProxy.Delete<GroupInfo>(primaryKeys);
        }

        public int Delete(int primaryKey, bool isLogic)
        {
            return DataAccessProxy.Delete<GroupInfo>(primaryKey);
        }

        public int Edit(GroupInfo entity)
        {
            return DataAccessProxy.Edit(entity);
        }
        public int EditColumns(GroupInfo entity)
        {
            return DataAccessProxy.Edit(entity, it => new GroupInfo() { GroupName = entity.GroupName }, it => it.Id == entity.Id);
        }
        public GroupInfo Get(int id)
        {
            return DataAccessProxy.Get<GroupInfo>(id);
        }
        public int GetCount(string where, object parameters)
        {
            return DataAccessProxy.GetCount<GroupInfo>(where, parameters);
        }
        public List<GroupInfo> GetList(string where, object parameters, string orderBy)
        {
            return DataAccessProxy.GetList<GroupInfo>(where, parameters, orderBy);
        }
        public List<GroupInfo> GetList(string where, object parameters, int pageIndex, int pageSize, string orderBy, ref int total)
        {
            //db.Queryable<Student>().Where("name=@name", new { name = value }).ToList();
            return DataAccessProxy.GetListPage<GroupInfo>(where, parameters, orderBy, pageIndex, pageSize, ref total);
        }
        public List<GroupInfo> GetList(string sql, object obj)
        {
            return DataAccessProxy.SqlQuery<GroupInfo>(sql, obj) as List<GroupInfo>;
        }

        public List<UserView> GetList()
        {
            return DataAccessProxy.GetList<UserInfo, GroupInfo, UserView>((st, sc) => new object[] {
              SqlSugar.JoinType.Left,st.GroupID==sc.Id
            }, st => st.Id == 1, (st, sc) => st.Id, (st, sc) => new UserView { Id = st.Id, CnName = st.CnName, GroupName = sc.GroupName });
        }
        public List<UserView> GetList2()
        {
            return DataAccessProxy.GetList<UserInfo, GroupInfo, DeptInfo, UserView>((st, sc, sd) => new object[] {
              SqlSugar.JoinType.Left,st.GroupID==sc.Id,
              SqlSugar.JoinType.Left,st.DeptID==sd.Id
            }, (st => st.Id == 1), null, (st, sc, sd) => new UserView { Id = st.Id, CnName = st.CnName, GroupName = sc.GroupName, DeptName = sd.DeptName });
        }
    }
}
