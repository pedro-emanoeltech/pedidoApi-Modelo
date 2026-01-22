using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Abastacao;
using Pedidos.Infra.Contexto;
using Pedidos.Infra.Interface;


namespace Pedidos.Infra.Servico
{
    public abstract class BaseRepositorio<TEntidade, TContexto> : IBaseRepositorio<TEntidade>
        where TEntidade : BaseEntidade, IEntidade
        where TContexto : BaseContexto
    {
        protected readonly TContexto _context;
        protected readonly DbSet<TEntidade> _dbSet;

        protected BaseRepositorio(TContexto context)
        {
            _context = context;
            _dbSet = context.Set<TEntidade>();
        }

        public virtual async Task<TEntidade> AdicionarAsync(TEntidade entidade, bool saveChanges = true)
        {
            try
            {
                await _context.Set<TEntidade>().AddAsync(entidade);
                await _dbSet.AddAsync(entidade);
                if (saveChanges)
                {
                    await _context.SaveChangesAsync();
                }
                return entidade;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual async Task<TEntidade> AtualizarAsync(TEntidade entidade, bool saveChanges = true)
        {
            try
            {
                _dbSet.Update(entidade);
                if (saveChanges)
                {
                    await _context.SaveChangesAsync();
                }

                return entidade;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
 
        public virtual async Task<TEntidade?> ObterPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id inválido", nameof(id));

            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
 
    }
}
