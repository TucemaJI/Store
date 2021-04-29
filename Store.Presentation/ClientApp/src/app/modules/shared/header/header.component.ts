import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { Observable, Observer } from 'rxjs';
import { RefreshToken } from '../../account/store/account.actions';
import { CartComponent } from '../../cart/componensts/cart/cart.component';
import { Consts } from '../consts';
import { AuthService } from '../services/auth.service';
import { ImageFromUrlService } from '../services/image-from-url.service';
import { ShoppingCartService } from '../services/shopping-cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  public userId: string;
  public count: number;
  public image: any;

  constructor(public dialog: MatDialog, private auth: AuthService, private router: Router, private store: Store, private cartService: ShoppingCartService<BaseCartItem>, private imageService: ImageFromUrlService) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    this.count = this.cartService.getItems().length;
    this.cartService.cartChanged.subscribe((count) => this.count = count);

    this.imageService.getBase64ImageFromURL(this.auth.getPhoto()).subscribe(base64data => {
      debugger;
      this.image = base64data;
    });

  }

  logOut(): void {
    localStorage.clear();
    this.userId = undefined;
    this.image = undefined;
    this.auth.signOutExternal();
    this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
  }

  cart(): void {
    this.dialog.open(CartComponent).updateSize(Consts.CART_DIALOG_SIZE);
  }

  check(): void {
    if (this.userId === undefined) {
      this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
    }
    if (!this.auth.isAuthenticated()) {
      const token = this.auth.getTokens();
      this.store.dispatch(new RefreshToken(token));
    }
  }
}
