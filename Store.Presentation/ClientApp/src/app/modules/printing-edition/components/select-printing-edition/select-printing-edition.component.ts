import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { Consts } from 'src/app/modules/shared/consts';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { GetPE } from '../../store/printing-edition.actions';

@Component({
  selector: 'app-select-printing-edition',
  templateUrl: './select-printing-edition.component.html',
  styleUrls: ['./select-printing-edition.component.css']
})
export class SelectPEComponent implements OnInit {

  public count: number = Consts.QUANTITY_MINIMUM_VALUE;
  public printingEdition: IPrintingEdition;
  public userId: string;

  constructor(private route: ActivatedRoute, private store: Store, private cartService: ShoppingCartService<BaseCartItem>, private router: Router, private auth: AuthService) { }
  ngOnInit() {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    const idStr = this.route.snapshot.paramMap.get(Consts.ID);
    const id = Number.parseInt(idStr);

    this.store.dispatch(new GetPE(id));
    this.selectPE(id);
  }

  add(): void {
    if (this.userId === undefined) {
      this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
    }
    if (this.count < Consts.QUANTITY_MINIMUM_VALUE) {
      alert(Consts.QUANTITY_MINIMUM)
      return;
    }

    const item = new BaseCartItem({ id: this.printingEdition.id, name: this.printingEdition.title, price: this.printingEdition.price, quantity: this.count, data: this.printingEdition.currencyType });

    if (this.cartService.isExist(this.printingEdition.id.toString())) {
      let itemFromCookie = this.cartService.getItem(this.printingEdition.id.toString());
      itemFromCookie.quantity += this.count;
      this.cartService.addItem(itemFromCookie);
      return;
    }
    this.cartService.addItem(item);
  }

  selectPE(id: number): void {
    this.store.subscribe(
      data => {
        if (data.printingEdition.printingEditions !== null) {
          this.printingEdition = data.printingEdition.printingEditions.find(el => el.id === id);
        }
      }
    );
  }
}