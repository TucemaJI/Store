import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { Consts } from '../consts';

@Injectable({
  providedIn: 'root'
})
export class ImageFromUrlService {

  constructor() { }



  public getBase64ImageFromURL(url: string): Observable<string> {
    return new Observable((observer: Observer<string>) => {
      let img = new Image();
      img.crossOrigin = Consts.IMG_CROSS_ORIGIN;
      debugger;
      img.src = url;
      img.onload = () => {
        observer.next(this.getBase64Image(img));
        observer.complete();
      };
      img.onerror = (err) => {
        observer.error(err);
      };
    });
  }

  private getBase64Image(img: HTMLImageElement): string {
    let canvas = document.createElement(Consts.CANVAS_TYPE) as HTMLCanvasElement;
    canvas.width = img.width;
    canvas.height = img.height;
    const context = canvas.getContext(Consts.DIMENSION_2) as CanvasRenderingContext2D;
    context.drawImage(img, 0, 0);
    return canvas.toDataURL(Consts.IMG_CONTENT_TYPE);
  }

}
