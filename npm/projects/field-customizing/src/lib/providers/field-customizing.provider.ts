import { APP_INITIALIZER } from '@angular/core';
import SIMPLE_TEXT_PROVIDERS from '../components/textbox/textbox.provider';
import { FieldCustomizingService } from '../services';

export const FIELD_CUSTOMIZING_PROVIDERS = [{
    provide: APP_INITIALIZER,
    useFactory: configureFieldProviders,
    deps: [FieldCustomizingService],
    multi: true,
}];

export function configureFieldProviders(fieldCustomizing: FieldCustomizingService) {
    return () => {
        fieldCustomizing.addes([
            ...SIMPLE_TEXT_PROVIDERS
        ]);
    };
}

