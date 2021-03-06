import { Component, Input, Output, EventEmitter } from '@angular/core';

import { of } from 'rxjs';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';

import { CartItem } from '@app/core';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.scss']
})
export class CartItemComponent {

  @Input() cartItem: CartItem;

  @Output() removeFromCart = new EventEmitter<CartItem>();
  @Output() quantityChange = new EventEmitter<CartItem>();

  onRemoveButtonClick() {
    this.removeFromCart.emit(this.cartItem);
  }

  onQuantityChange(quantity: number) {
    of(quantity).pipe(
      debounceTime(1000),
      distinctUntilChanged()
    ).subscribe(() => this.quantityChange.emit(this.cartItem));
  }

}
