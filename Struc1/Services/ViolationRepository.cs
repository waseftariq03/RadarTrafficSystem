using DAL.DB;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Struc.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Struc.Services

{

    public class ViolationRepository : IViolationRepository

    {

        private readonly DatabaseContext _context;
        private readonly ILogger<ViolationRepository> _logger;

        public ViolationRepository(DatabaseContext context, ILogger<ViolationRepository> logger)

        {

            _context = context;
            _logger = logger;

        }

        public async Task SaveAsync(Violation violation)

        {

            if (violation == null)

            {

                _logger.LogError("Attempted to save a null violation.");
                throw new ArgumentNullException(nameof(violation), "Violation cannot be null.");

            }

            await _context.Violations.AddAsync(violation);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Violation saved: PlateNumber={Plate}", violation.PlateNumber);

        }

        public async Task<List<Violation>> GetAllAsync() =>

            await _context.Violations.AsNoTracking().ToListAsync();

    }

}

