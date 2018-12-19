﻿using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Core.Managers.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Modules.Cart
{
    [ApiController]
    [Route("api/users/{userId}/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager cartManager;
        private readonly IMapper mapper;

        public CartController(ICartManager cartManager, IMapper mapper)
        {
            this.cartManager = cartManager;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CartDTO))]
        public IActionResult GetCart(Guid userId)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            var cartDto = this.mapper.Map<CartDTO>(cart);

            return Ok(cartDto);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CartDTO))]
        public IActionResult UpdateCart(Guid userId, [FromBody]IEnumerable<CartItemDTO> cartItemsDto)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            var cartItems = this.mapper.Map<IEnumerable<CartItem>>(cartItemsDto);

            this.cartManager.UpdateCartItems(cart, cartItems);

            var updatedCart = this.cartManager.GetCartByUserId(userId);
            var updatedCartDto = this.mapper.Map<CartDTO>(updatedCart);

            return Ok(updatedCartDto);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult ClearCart(Guid userId)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            this.cartManager.ClearCart(cart);

            return Ok();
        }

        [HttpPut("add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CartItemDTO))]
        public IActionResult AddToCart(Guid userId, [FromBody]CartItemDTO cartItemDto)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            this.cartManager.AddToCart(cart, cartItemDto.Product.Id, cartItemDto.Quantity);

            var cartItem = this.cartManager.GetCartItemByProductId(cartItemDto.Product.Id);
            var updatedCartItemDto = this.mapper.Map<CartItemDTO>(cartItem);

            return Ok(updatedCartItemDto);
        }

        [HttpPut("remove")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult RemoveFromCart(Guid userId, [FromBody]CartItemDTO cartItemDto)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            this.cartManager.RemoveFromCart(cart, cartItemDto.Product.Id);

            return Ok();
        }

        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CartItemDTO))]
        public IActionResult UpdateCartItem(Guid userId, [FromBody]CartItemDTO cartItemDto)
        {
            var cart = this.cartManager.GetCartByUserId(userId);
            this.cartManager.UpdateCartItem(cart, cartItemDto.Product.Id, cartItemDto.Quantity);

            var cartItem = this.cartManager.GetCartItemByProductId(cartItemDto.Product.Id);
            var updatedCartItemDto = this.mapper.Map<CartItemDTO>(cartItem);

            return Ok(updatedCartItemDto);
        }
    }
}
