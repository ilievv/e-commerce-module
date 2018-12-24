import { Component, Input } from '@angular/core';

import { CartItem } from '@app/core';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.scss']
})
export class CartItemComponent {

  @Input() cartItem: CartItem;

}
