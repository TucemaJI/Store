import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { pipe } from 'rxjs';
import { filter } from 'rxjs/operators';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { IAppState } from 'src/app/store/state/app.state';
import { getPE } from '../../store/printing-edition.actions';
import { selectPrintingEdition } from '../../store/printing-edition.selector';

@Component({
  selector: 'app-select-pe',
  templateUrl: './select-pe.component.html',
  styleUrls: ['./select-pe.component.css']
})
export class SelectPEComponent implements OnInit {

  printingEdition: IPrintingEdition = {
    authors: [],
    description: "",
    title: "",
    currencyType: 0,
    id: 0,
    price: 0,
    type: 0,
  };

  constructor(private route: ActivatedRoute, private store: Store<IAppState>) { }
// HERE IS THE PROBLEM WITH PRIORITY
  ngOnInit(): void {
    const idStr = this.route.snapshot.paramMap.get('id');
    const id = Number.parseInt(idStr);
    debugger;
    this.store.pipe(select(selectPrintingEdition, id), filter(val => val !== null)).subscribe(
      data => {
        debugger;
        this.printingEdition = data;
      }
    );
    if (this.printingEdition === undefined) {
      this.store.dispatch(getPE({ id }));
      debugger;
      this.store.pipe(select(selectPrintingEdition, id)).subscribe(
        data => {
          if (data !== null) {
            debugger;
            this.printingEdition = data;
          }
        }
      );
    }
  }
}