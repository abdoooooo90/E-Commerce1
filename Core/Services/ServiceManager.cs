using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ServiceManager : IServiceManager
	{
		private readonly Lazy<IProductService> _proudctService;
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _proudctService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        }
        public IProductService ProductService => _proudctService.Value;
	}
}
