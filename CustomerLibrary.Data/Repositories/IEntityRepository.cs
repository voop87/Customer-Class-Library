using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLibrary.Data
{
    public interface IEntityRepository<TEntity>
    {
        public int Create(TEntity entity);

        public TEntity Read(int entityId);

        public List<TEntity> ReadAll();

        public List<TEntity> ReadAll(int entityId);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);
    }
}
