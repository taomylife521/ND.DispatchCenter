using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ND.DispatchCenter.Core.Helper
{
    public class MongoDbHelper<T> where T : class
    {
        private static readonly string connectionString;// = ConfigurationManager.ConnectionStrings["MongoConnStr"].ConnectionString;
        private static readonly string dbName; //= ConfigurationManager.AppSettings["MongoDbName"].ToString();
        protected static MongoServer mongo = null;
        public static MongoDatabase db = null;
        public MongoCollection<T> collection = null;
        private static MongoDbHelper<T> _instance { get; set; }
        public static MongoDbHelper<T> Instance { get { return _instance; } }//默认依赖对象

        public MongoDbHelper()
        { 
          
        }
        static  MongoDbHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MongoConnStr"].ConnectionString;
            dbName = ConfigurationManager.AppSettings["MongoDbName"].ToString();
            mongo = MongoServer.Create(connectionString);
            db = mongo.GetDatabase(dbName);
            _instance = new MongoDbHelper<T>();
        }
        public MongoDbHelper(string collectionName,bool isEnsureExist=false):this()
        {
            if (isEnsureExist)
            {
                bool flag = db.CollectionExists(collectionName);
                if (!flag)//不存在
                {
                   db.CreateCollection(collectionName);
                }
            }
            collection = db.GetCollection<T>(collectionName);
        }

     

        #region 添加

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="entiry">插入的实体</param>
        public bool InsertOne(T entity)
        {
            try
            {
                collection.Insert(entity);
                return true;
            }
            finally
            {

                mongo.Disconnect();

            }


        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="entity"></param>
        public void InsertBatch(List<T> listEntity,bool isClear=false)
        {
            if (isClear)
            {
                collection.RemoveAll();
            }
            var result = collection.InsertBatch<T>(listEntity);
            mongo.Disconnect();
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetModelById(string id)
        {
            return collection.FindOneById(id);
        }

        /// <summary>
        /// 获取第一条记录(自定义条件)
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            IMongoQuery query = Query<T>.Where(expression);
            return collection.FindOne(query);
        }

        // <summary>
        /// 获取第一条记录
        /// </summary>
        /// <returns></returns>
        public T FirstOrDefault()
        {
            return collection.FindAll().FirstOrDefault();
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public List<T> FindAll()
        {
            
            
            return collection.FindAll().ToList();
        }

        /// <summary>
        /// 获取全部(自定义条件)
        /// </summary>
        /// <returns></returns>
        public List<T> FindAll(Expression<Func<T, bool>> expression)
        {
            IMongoQuery query = Query<T>.Where(expression);
            
            return collection.Find(query).ToList();
        }

        /// <summary>
        /// 根据条件获取数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public long GetCount(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return collection.Count();
            }
            else
            {
                return collection.Count(Query<T>.Where(expression));
            }
        }

        /// <summary>
        /// 根据ID判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string id)
        {
            return collection.FindOneById(id) != null;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex">总页码</param>
        /// <param name="PageSize">页容量</param>
        /// <param name="RowCounts">总记录数</param>
        /// <param name="expression">条件</param>
        /// <param name="IsAsc">是否是正序</param>
        /// <param name="OrderFiled">排序的字段</param>
        /// <returns></returns>
        public List<T> Page(int PageIndex, int PageSize, out  long RowCounts, Expression<Func<T, bool>> expression = null, bool IsAsc = true, params string[] OrderFiled)
        {
            MongoCursor<T> mongoCursor;

            //条件选择
            if (expression != null)
            {
                RowCounts = collection.Find(Query<T>.Where(expression)).Count();
                mongoCursor = collection.Find(Query<T>.Where(expression));
            }
            else
            {
                RowCounts = collection.FindAll().Count();
                mongoCursor = collection.FindAll();
            }

            //排序
            if (OrderFiled != null && OrderFiled.Length > 0)
            {
                //处理主键字段
                for (int i = 0; i < OrderFiled.Length; i++)
                {
                    if (OrderFiled[i].Equals("id", StringComparison.CurrentCultureIgnoreCase))
                    {
                        OrderFiled[i] = "_id";
                    }
                }

                if (IsAsc)
                {
                    mongoCursor = mongoCursor.SetSortOrder(SortBy.Ascending(OrderFiled));
                }
                else
                {
                    mongoCursor = mongoCursor.SetSortOrder(SortBy.Descending(OrderFiled));
                }
            }

            return mongoCursor.SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize).ToList();
        }

        #region 效率低，暂时不用
        ///// <summary>
        ///// 分页
        ///// </summary>
        ///// <returns></returns>
        //public List<T> Page(int PageIndex, int PageSize, out  long RowCounts, Expression<Func<T, bool>> expression = null)
        //{
        //    List<T> ret = new List<T>();
        //    IQueryable<T> queryable;
        //    //条件选择
        //    if (expression != null)
        //    {
        //        queryable = collection.Find(Query<T>.Where(expression)).AsQueryable();
        //    }
        //    else
        //    {
        //        queryable = collection.FindAll().AsQueryable();
        //    }
        //    RowCounts = queryable.Count();
        //    ret = queryable.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        //    return ret;
        //}

        ///// <summary>
        ///// 分页
        ///// </summary>
        ///// <typeparam name="TKey"></typeparam>
        ///// <param name="PageIndex"></param>
        ///// <param name="PageSize"></param>
        ///// <param name="RowCounts"></param>
        ///// <param name="expression"></param>
        ///// <param name="orderBy"></param>
        ///// <param name="IsOrder"></param>
        ///// <returns></returns>
        //public List<T> Page<TKey>(int PageIndex, int PageSize, out  long RowCounts, Expression<Func<T, bool>> expression = null, Expression<Func<T, TKey>> orderBy = null, bool IsOrder = true)
        //{
        //    List<T> ret = new List<T>();
        //    IQueryable<T> queryable;

        //    //条件选择
        //    if (expression != null)
        //    {
        //        queryable = collection.Find(Query<T>.Where(expression)).AsQueryable();
        //    }
        //    else
        //    {
        //        queryable = collection.FindAll().AsQueryable();
        //    }
        //    //排序
        //    if (orderBy != null)
        //    {
        //        if (IsOrder)
        //        {
        //            queryable = queryable.OrderBy(orderBy);
        //        }
        //        else
        //        {
        //            queryable = queryable.OrderByDescending(orderBy);
        //        }
        //    }
        //    RowCounts = queryable.Count();
        //    ret = queryable.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        //    return ret;
        //} 
        #endregion

        #region 自定义，不用可删除

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public T GetOne(string collectionName, QueryDocument query)
        {
            T result = default(T);
            //MongoServer mongo = MongoServer.Create(connectionString);
            //mongo.Connect();
            //MongoDatabase friends = mongo.GetDatabase(database);
            //MongoCollection<T> categories = friends.GetCollection<T>(collectionName);
            result = collection.FindOneAs<T>(query);
            mongo.Disconnect();

            return result;
        }


        /// <summary>
        /// 根据条件获取多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        //public static List<T> GetAllByQuery<T>(string collectionName, QueryDocument query)
        //{
        //    List<T> result = new List<T>();
        //    //MongoServer mongo = MongoServer.Create(connectionString);
        //    //mongo.Connect();
        //    //MongoDatabase friends = mongo.GetDatabase(database);
        //    //MongoCollection<T> categories = friends.GetCollection<T>(collectionName);
        //    foreach (var entity in collection.Find(query))
        //    {
        //        //result.Add(entity as T);
        //    }
        //    mongo.Disconnect();

        //    return result;
        //}


        ///// <summary>
        ///// 获取一条数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collectionName"></param>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //public static T GetOne<T>(string collectionName, Document query, Document fields) where T : class
        //{
        //    T result = default(T);
        //    using (Mongo mongo = new Mongo(connectionString))
        //    {
        //        mongo.Connect();
        //        IMongoDatabase friends = mongo.GetDatabase(database);
        //        IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
        //        result = categories.Find(query, fields).Skip(0).Limit(1).Documents.First();
        //        mongo.Disconnect();

        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 获取一个集合下所有数据
        ///// </summary>
        ///// <param name="collectionName"></param>
        ///// <returns></returns>
        //public static List<T> GetAll<T>(string collectionName) where T : class
        //{
        //    List<T> result = new List<T>();
        //    using (Mongo mongo = new Mongo(connectionString))
        //    {
        //        mongo.Connect();
        //        IMongoDatabase friends = mongo.GetDatabase(database);
        //        IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
        //        foreach (T entity in categories.FindAll().Limit(50).Documents)
        //        {
        //            result.Add(entity);
        //        }
        //        mongo.Disconnect();

        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collectionName"></param>
        ///// <param name="query"></param>
        ///// <param name="Sort"></param>
        ///// <param name="cp"></param>
        ///// <param name="mp"></param>
        ///// <returns></returns>
        //public static List<T> GetList<T>(string collectionName, object selector, Document sort, int cp, int mp) where T : class
        //{
        //    List<T> result = new List<T>();
        //    using (Mongo mongo = new Mongo(connectionString))
        //    {
        //        mongo.Connect();
        //        IMongoDatabase friends = mongo.GetDatabase(database);
        //        IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
        //        foreach (T entity in categories.Find(selector).Sort(sort).Skip((cp - 1) * mp).Limit(mp).Documents)
        //        {
        //            result.Add(entity);
        //        }
        //        mongo.Disconnect();

        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collectionName"></param>
        ///// <param name="query"></param>
        ///// <param name="Sort"></param>
        ///// <param name="cp"></param>
        ///// <param name="mp"></param>
        ///// <returns></returns>
        //public static List<T> GetList<T>(string collectionName, object selector, object fields, Document sort, int cp, int mp) where T : class
        //{
        //    List<T> result = new List<T>();
        //    using (Mongo mongo = new Mongo(connectionString))
        //    {
        //        mongo.Connect();
        //        IMongoDatabase friends = mongo.GetDatabase(database);
        //        IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
        //        foreach (T entity in categories.Find(selector, fields).Sort(sort).Skip((cp - 1) * mp).Limit(mp).Documents)
        //        {
        //            result.Add(entity);
        //        }
        //        mongo.Disconnect();

        //    }
        //    return result;
        //}

        #endregion
        #endregion

        #region 删除
        /// <summary>
        /// 带条件的删除
        /// </summary>
        /// <param name="expression"></param>
        public void Delete(Expression<Func<T, bool>> expression)
        {
            IMongoQuery query = Query<T>.Where(expression);
            var result = collection.Remove(query);
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        public void DeleteAll()
        {
            var result = collection.RemoveAll();
        }

        #region 需继承 IMongoEntity
        
        /// <summary>
        /// 根据模型删除
        /// </summary>
        /// <param name="model"></param>
        //public void Delete(T model)
        //{
        //    MongoDB.Driver.IMongoQuery query = Query<T>.Where(p => p.Id == model.Id);
        //    collection.Remove(query);
        //}

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="Id"></param>
        //public void Delete(string Id)
        //{
        //    MongoDB.Driver.IMongoQuery query = Query<T>.Where(p => p.Id == Id);
        //    collection.Remove(query);
        //}

        /// <summary>
        /// 全部删除
        /// </summary>
        /// <returns></returns>
        /// 

        #endregion
        
        #region 自定义 删除
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="entity"></param>
        public void Delete(string collectionName, QueryDocument query)
        {

            //MongoServer mongo = MongoServer.Create(connectionString);
            //mongo.Connect();
            //MongoDatabase friends = mongo.GetDatabase(database);
            //MongoCollection<T> categories = friends.GetCollection<T>(collectionName);
            collection.Remove(query);
            mongo.Disconnect();

        }
        #endregion

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(T model)
        {
            var result = collection.Save<T>(model);
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        public void UpdateAll(List<T> model)
        {
            model.ForEach(e => collection.Save<T>(e));
        }


        #region 自定义 方法
        
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询条件</param>
        /// <param name="setValue">要赋的值</param>
        /// <param name="isMulti">是否更新多行</param>
        public void Update(string collectionName, IMongoQuery query, QueryDocument setValue, bool isMulti)
        {
            //MongoServer mongo = MongoServer.Create(connectionString);

            //mongo.Connect();
            //MongoDatabase friends = mongo.GetDatabase(database);
            //MongoCollection<T> categories = friends.GetCollection<T>(collectionName);
            //定义更新文档
            IMongoUpdate update = new UpdateDocument { { "$set", setValue } };

            //执行更新操作
            if (isMulti)
                collection.Update(query, update, UpdateFlags.Multi);
            else
                collection.Update(query, update, UpdateFlags.None);
        }

        #endregion

        #endregion
    }

}
