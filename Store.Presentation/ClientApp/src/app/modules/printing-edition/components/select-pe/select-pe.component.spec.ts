import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectPEComponent } from './select-pe.component';

describe('SelectPEComponent', () => {
  let component: SelectPEComponent;
  let fixture: ComponentFixture<SelectPEComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectPEComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectPEComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
