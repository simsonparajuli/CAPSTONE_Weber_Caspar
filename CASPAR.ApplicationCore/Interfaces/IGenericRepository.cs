using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Interfaces
{
    public interface IGenericRepository<T>
    {
        // Get the object by its key id
        T GetById(int? id);

        /* The following method will return a single object using an Expression filter (similar to a WHERE clause in SQL)

        Func<T, bool> represents a function that takes an object of generic type T and returns a bool indicating whether filter exists or not*

        Expression<Func<T>> is a description of a function as an expression tree.
                 https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/expression-trees-explained

        The expression is commonly referred to as a predicate and is used to verify a condition on an object.
         https://learn.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-7.0

         *The advantage is that Func<T> can be evaluated and compiled at run time and translated to other languages e.g. SQL in LINQ to SQL.

        NoTracking is ReadOnly Results (we're not normally tracking changes, but can do so if we are updating and what to synchronize that’s changed between the reading and writing of data)

        Includes is used similarly to a SQL JOIN to “connect and relate to” other objects (PK-FK)
        */

        T Get(Expression<Func<T, bool>> predicate, bool asNotTracking = false, string includes = null);

        //Same as Getl but Async call
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNotTracking = false, string includes = null);

        /*The following returns an Enumerable list of objects to iterate through.
        Expression is the same as the Get Method
        A second Expression is added for an Order By (integer results)
        Includes is the same as we’ve seen before and is used to join other objects */

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, Expression<Func<T, int>> orderBy = null, string includes = null);

        // Same as above but Asynchronous action
        /* Asynchronous calls are most useful when facing relatively infrequent large, expensive operations that could tie up response threads that could otherwise be servicing requests while the originator waits. For quick, common operations, async can slow things down. */

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        // Add (Insert) a new record instance
        void Add(T entity);

        // Delete (Remove) a single record instance
        void Delete(T entity);

        // Delete (Remove) multiple record instances
        void Delete(IEnumerable<T> entities);

        // Update all changes to an object
        void Update(T entity);

        public IQueryable<T> Query();
    }
}
