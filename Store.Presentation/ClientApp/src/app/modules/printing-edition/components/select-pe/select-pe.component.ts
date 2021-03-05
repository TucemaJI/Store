import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { pipe } from 'rxjs';
import { filter } from 'rxjs/operators';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { IAppState } from 'src/app/store/state/app.state';
import { getPE, getPEs } from '../../store/printing-edition.actions';
import { selectPrintingEditions } from '../../store/printing-edition.selector';

@Component({
  selector: 'app-select-pe',
  templateUrl: './select-pe.component.html',
  styleUrls: ['./select-pe.component.css']
})
export class SelectPEComponent implements OnInit {

  id: number;
  count: number = 1;
  printingEdition: IPrintingEdition;

  constructor(private route: ActivatedRoute, private store: Store<IAppState>) { }
  ngOnInit() {
    const idStr = this.route.snapshot.paramMap.get('id');
    //const id = Number.parseInt(idStr);
    this.id = Number.parseInt(idStr);
    debugger;
    //this.selectPE(id);
    if (this.printingEdition === undefined) {
      this.store.dispatch(getPE({ id: this.id }));
      debugger;
    }
  }

  selectPE$ = this.store.pipe(select(selectPrintingEditions)).subscribe(
    data => {
      debugger;
      if (data.printingEditions !== null) {
        debugger;
        this.printingEdition = data.printingEditions.find(el => el.id === this.id);
      }
    }
  );

  add() {
    console.log(this.count);
  }

  // selectPE(id: number) {
  //   this.store.pipe(select(selectPrintingEditions)).subscribe(
  //     data => {
  //       if (data.printingEditions !== null) {
  //         debugger;
  //         this.printingEdition = data.printingEditions.find(el => el.id === id);
  //       }
  //     }
  //   );
  // }


}