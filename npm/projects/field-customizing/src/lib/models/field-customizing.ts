import { Type } from '@angular/core';

// tslint:disable-next-line:no-namespace
export namespace FieldCustomizing {

    export type FormConfiguration<T> = T & FormConfigurationBase;

    export interface FormConfigurationBase {
        required: boolean;
        description: string;
    }

    export interface FormProviderName {
        formProviderName: string;
    }

    export interface FormProvider<T = FormConfigurationBase> {
        name: string;
        displayName: string;
        useType: FormProviderUseType;
        component: Type<FormProviderComponent<T>>;
    }

    export abstract class FormProviderComponent<T = FormConfigurationBase>{
        formConfig: T;
    }

    export type FormProviderUseType = 'view' | 'form' | string;
}
