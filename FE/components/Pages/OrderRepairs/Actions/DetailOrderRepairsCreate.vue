<template>
  <div>
    <a-button @click="showModal">Show</a-button>
    <a-modal
      title="Chi tiết máy"
      :visible="visible"
      :confirm-loading="isLoading"
      @ok="handleSubmit"
      @cancel="handleCancel"
      class="fc-modal"
      :destroyOnClose="true"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancel" size="large">Hủy</a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="isLoading"
          @click="handleSubmit"
          size="large"
        >
          Lưu
        </a-button>
      </template>

      <a-form :form="form" @submit="handleSubmit">
        <detail-order-repairs-form />
      </a-form>
    </a-modal>
  </div>
</template>

<script>
import DetailOrderRepairsForm from "../Core/DetailOrderRepairsForm.vue";
export default {
  name: "DetailOrderRepairsCreate",
  components: {
    DetailOrderRepairsForm,
  },
  props: {
    keyIndex: {
      type: Number,
    },
  },
  data() {
    return {
      visible: false,
      isLoading: false,
      form: this.$form.createForm(this),
    };
  },
  methods: {
    showModal() {
      this.visible = true;
    },
    handleCancel(e) {
      this.visible = false;
    },
    handleSubmit() {
      this.form.validateFields((err, values) => {
        if (!err) {
          this.$emit("onSubmitData", values, this.keyIndex);
          this.handleCancel();
        } else this.$message.error("Đã xảy ra lỗi, vui lòng thử lại!");
      });
    },
  },
};
</script>

<style></style>
