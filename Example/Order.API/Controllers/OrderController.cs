using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JieDDDFramework.Data.EntityFramework;
using JieDDDFramework.Data.Repository;
using JieDDDFramework.Web;
using JieDDDFramework.Web.Models;
using JieDDDFramework.Web.ModelValidate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Application.Commands;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryBase<Domain.Aggregates.OrderAggregate.Order> _repositoryBase;

        public OrderController(IMediator mediator, IRepositoryBase<Domain.Aggregates.OrderAggregate.Order> repositoryBase)
        {
            _mediator = mediator;
            _repositoryBase = repositoryBase;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost,Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return result?Success():Fail();
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet,Route("")]
        public async Task<IActionResult> Orders([FromQuery, Validator] PagedCriteria criteria)
        {
            //在正式项目中不建议暴露领域对象，建议加入Application层，用于协调WebApi与领域对象
            var result = await _repositoryBase.Tables()
                .OrderByCreatedTime()
                .Select(x => new //在正式项目中应创建DTO对象来进行映射，可使用automapper组件
                {
                    OrderId = x.Id,
                    PaymentType = x.PaymentMethod.PaymentType.Name,
                    x.CreatedTime,
                    x.Address,
                    Buyer = x.Buyer.Name,
                    Item = x.OrderItems.Select(o => new {o.ProductId, o.ProductName, o.ProductCount, o.ProductPrice})
                })
                .ToPageResult(criteria.PageIndex, criteria.PageSize);
            return Success(result);
        }
    }
}
