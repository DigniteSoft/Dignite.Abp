import { ABP } from '@abp/ng.core';

export class SetCurrentAppState {
    static readonly type = '[SetCurrentAppState] Set';
    constructor(public payload: ABP.Route) { }
}
