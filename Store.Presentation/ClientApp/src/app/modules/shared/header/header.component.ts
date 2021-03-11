import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { CartComponent } from '../../cart/componensts/cart/cart.component';
import { selectUser } from '../../printing-edition/store/printing-edition.selector';
import { User } from '../models/User';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  user: User;

  constructor(public dialog: MatDialog, private store: Store<IAppState>) { }

  ngOnInit(): void {
  }

  selectUser() {
    this.store.pipe(select(selectUser)).subscribe(
      data => {
        if (data !== null) {
          debugger;
          this.user = data;
        }
      }
    );
  }

  cart() {
    const dialog = this.dialog.open(CartComponent).updateSize("300%");
  }

}
