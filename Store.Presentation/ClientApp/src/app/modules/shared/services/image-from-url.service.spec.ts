import { TestBed } from '@angular/core/testing';

import { ImageFromUrlService } from './image-from-url.service';

describe('ImageFromUrlService', () => {
  let service: ImageFromUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ImageFromUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
