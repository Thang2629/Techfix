<template>
  <div class="customer-form">
    <a-form-item v-if="!isEditView" label="Nhóm khách hàng">
      <a-select
        v-decorator="[
          'customer_type',
          {
            initialValue: dataCustomerType[0].id,
          },
        ]"
        size="large"
        class="fc-select-normal"
        :getPopupContainer="(trigger) => trigger.parentNode"
      >
        <a-select-option
          v-for="cType in dataCustomerType"
          :key="cType.id"
          :value="cType.id"
        >
          {{ cType.name }}
        </a-select-option>
      </a-select>
    </a-form-item>

    <a-form-item label="Tên khách hàng">
      <a-input
        v-decorator="[
          'c_name',
          {
            rules: [
              { required: true, message: 'Vui lòng nhập tên khách hàng' },
            ],
            initialValue: detail?.c_name,
          },
        ]"
        placeholder="Nhập tên khách hàng"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Số điện thoại">
      <a-input
        v-decorator="[
          'c_phone',
          {
            initialValue: detail?.c_phone,
          },
        ]"
        placeholder="Nhập số điện thoại khách hàng"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Ngày sinh">
      <a-date-picker
        format="DD/MM/YYYY"
        v-decorator="[
          'c_birthday',
          {
            initialValue: detail?.c_birthday,
          },
        ]"
        placeholder="Chọn ngày sinh"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Giới tính">
      <a-radio-group
        v-decorator="[
          'c_gender',
          {
            initialValue: detail?.c_gender,
          },
        ]"
      >
        <a-radio value="Nam ">Nam</a-radio>
        <a-radio value="Nữ">Nữ</a-radio>
      </a-radio-group>
    </a-form-item>

    <a-form-item label="Email">
      <a-input
        v-decorator="[
          'c_email',
          {
            initialValue: detail?.c_email,
          },
        ]"
        placeholder="Nhập email khách hàng"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Địa chỉ">
      <a-input
        v-decorator="[
          'c_address',
          {
            initialValue: detail?.c_address,
          },
        ]"
        placeholder="Nhập địa chỉ khách hàng"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Ghi chú">
      <a-input
        v-decorator="[
          'notes',
          {
            initialValue: detail?.notes,
          },
        ]"
        placeholder="Nhập ghi chú"
        size="large"
        allow-clear
      />
    </a-form-item>
  </div>
</template>

<script>
export default {
  name: "CustomerForm",
  props: {
    detail: {
      type: Object,
      default: () => {},
    },
    isEditView: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    dataCustomerType() {
      const cType = [
        {
          id: 0,
          name: "Khách lẻ",
        },
        {
          id: 1,
          name: "Khách sỉ",
        },
      ];
      return cType;
    },
  },
};
</script>

<style lang="less">
.customer-form {
  .ant-form-item {
    margin-bottom: 5px;
  }
}
</style>
