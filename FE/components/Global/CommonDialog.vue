<template>
  <a-modal
    :title="title"
    :visible="visible"
    :confirm-loading="isLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :destroyOnClose="true"
  >
    <slot />
  </a-modal>
</template>
  
<script>
export default {
  props: {
    title: {
      type: String,
      default: '',
    },
    visible: {
      type: Boolean,
      default: false,
    },
    isLoading: {
      type: Boolean,
      default: false,
    },
  },
  methods: {
    handleSubmit() {
      this.$emit('handle-submit');
    },
    handleCancel() {
      this.$emit('handle-cancel');
    },
  },
};
</script>
    