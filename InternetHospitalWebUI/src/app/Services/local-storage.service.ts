import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class LocalStorageService {
    private storageSub = new Subject<string>();

    watchStorage(): Observable<any> {
        return this.storageSub.asObservable();
    }

    setItem(key: string, data: any, avatar: string) {
        localStorage.setItem(key, data);
        this.storageSub.next(avatar);
    }

}
