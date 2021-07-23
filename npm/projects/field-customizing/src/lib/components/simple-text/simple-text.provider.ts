import { FieldCustomizing } from '../../models/field-customizing';
import { SimpleTextViewComponent } from './simple-text-view.component';

/**
 * 简单form组件
 */
const SIMPLE_TEXT_PROVIDERS: FieldCustomizing.FormProvider[] = [{
    displayName: '简单组件',
    name: 'simple-text',
    useType: 'view',
    component: SimpleTextViewComponent
}, {
    displayName: '简单组件',
    name: 'simple-text',
    useType: 'form',
    component: SimpleTextViewComponent
}];

export default SIMPLE_TEXT_PROVIDERS;
