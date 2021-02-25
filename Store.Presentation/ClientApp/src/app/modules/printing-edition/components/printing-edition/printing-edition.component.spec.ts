import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintingEditionComponent } from './printing-edition.component';

describe('PrintingEditionComponent', () => {
  let component: PrintingEditionComponent;
  let fixture: ComponentFixture<PrintingEditionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrintingEditionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintingEditionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
