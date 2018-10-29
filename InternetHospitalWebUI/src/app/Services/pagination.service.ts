import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { Pagination } from '../Models/Pagination';
@Injectable({
    providedIn: 'root'
})
export class PaginationService {
    private paginationModel: Pagination;

    get pageIndex(): number {
        return this.paginationModel.pageIndex;
    }
    set pageIndex(num: number) {
        this.paginationModel.pageIndex = num;
    }
    get pageSize(): number {
        return this.paginationModel.pageSize;
    }

    constructor() {
        this.paginationModel = new Pagination();
    }

    change(pageEvent: PageEvent) {
        this.paginationModel.pageIndex = pageEvent.pageIndex + 1;
        this.paginationModel.pageSize = pageEvent.pageSize;
        this.paginationModel.allItemsLength = pageEvent.length;
    }
}
