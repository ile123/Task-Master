import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataStorage {
  private dataStorage: any = {};
  private dataChanged = new Subject<void>();

  addData(key: string, data: any) {
    this.dataStorage[key] = data;
    if(key == "role") {
      this.dataChanged.next();
    }
  }

  getData(key: string) {
    return this.dataStorage[key];
  }

  clearData(key: string) {
    this.dataStorage[key] = undefined;
  }

  dataChanged$(): Observable<void> {
    return this.dataChanged.asObservable();
  }
}
