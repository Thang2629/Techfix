<template>
  <div>
    <a-alert message="Chi tiết đơn sửa chữa" type="info" show-icon />
    <a-button type="primary" style="margin: 1rem 0" @click="handleAdd">
      Thêm
    </a-button>
    <a-table
      :columns="columns"
      :data-source="data"
      bordered
      :pagination="false"
    >
      <template
        v-for="col in ['pR_code', 'pR_Name', 'pR_status_error']"
        :slot="col"
        slot-scope="text, record"
      >
        <div :key="col">
          <a-input
            :value="text"
            @change="(e) => handleChange(e.target.value, record.key, col)"
          />
        </div>
      </template>

      <template slot="key" slot-scope="text, record, index">
        {{ index + 1 }}
      </template>

      <template slot="pR_price" slot-scope="text, record, index">
        <repair-price-input
          :keyIndex="index"
          v-on:onChangePrice="onChangePrice"
        />
      </template>

      <template slot="pR_repair_type" slot-scope="text, record, index">
        <repair-type-select
          v-on:onChangeRepairType="onChangeRepairType"
          :keyIndex="index"
        />
      </template>

      <template slot="pR_date_finish" slot-scope="text, record, index">
        <repair-date-finish-input
          :keyIndex="index"
          v-on:onChangeDateFinish="onChangeDateFinish"
        />
      </template>

      <template slot="detail_product_pepairs" slot-scope="text, record, index">
        <detail-order-repairs-create
          v-on:onSubmitData="onSubmitData"
          :keyIndex="index"
        />
      </template>

      <template slot="operation" slot-scope="text, record">
        <a-tooltip title="Xóa">
          <a-icon
            type="delete"
            class="btn-delete"
            :theme="'twoTone'"
            :twoToneColor="'#FB4E4E'"
            @click="onDelete(record.key)"
          />
        </a-tooltip>
      </template>
    </a-table>

    <repair-customer-select
      v-on:onSelectCustomer="onSelectCustomer"
      v-on:onChangeNote="onChangeNote"
    />

    <a-divider></a-divider>
    <div id="order-repair-form-footer">
      <nuxt-link to="/order_repairs">
        <a-button key="back" size="large"> Hủy </a-button>
      </nuxt-link>

      <a-button
        style="margin-left: 1rem"
        key="submit"
        type="primary"
        :loading="isLoading"
        @click="handleSubmit"
        size="large"
      >
        Lưu
      </a-button>
    </div>
  </div>
</template>
<script>
import moment from "moment";

import DetailOrderRepairsCreate from "../Actions/DetailOrderRepairsCreate.vue";
import RepairTypeSelect from "../Actions/RepairTypeSelect.vue";
import RepairPriceInput from "../Actions/RepairPriceInput.vue";
import RepairDateFinishInput from "../Actions/RepairDateFinishInput.vue";
import RepairCustomerSelect from "../Actions/RepairCustomerSelect.vue";

const columns = [
  {
    title: "STT",
    dataIndex: "key",
    scopedSlots: { customRender: "key" },
  },
  {
    title: "Mã SP",
    dataIndex: "pR_code",
    scopedSlots: { customRender: "pR_code" },
  },
  {
    title: "Tên SP",
    dataIndex: "pR_Name",
    scopedSlots: { customRender: "pR_Name" },
  },
  {
    title: "Chi tiết máy",
    dataIndex: "detail_product_pepairs",
    scopedSlots: { customRender: "detail_product_pepairs" },
  },
  {
    title: "Loại sửa",
    dataIndex: "pR_repair_type",
    scopedSlots: { customRender: "pR_repair_type" },
  },
  {
    title: "Tình trạng lỗi",
    dataIndex: "pR_status_error",
    scopedSlots: { customRender: "pR_status_error" },
  },
  {
    title: "Giá sửa",
    dataIndex: "pR_price",
    scopedSlots: { customRender: "pR_price" },
  },
  {
    title: "Ngày sửa xong",
    dataIndex: "pR_date_finish",
    scopedSlots: { customRender: "pR_date_finish" },
  },
  {
    title: "",
    dataIndex: "operation",
    scopedSlots: { customRender: "operation" },
  },
];

const data = [];
const count = 0;

export default {
  components: {
    DetailOrderRepairsCreate,
    RepairTypeSelect,
    RepairPriceInput,
    RepairDateFinishInput,
    RepairCustomerSelect,
  },
  data() {
    return {
      data,
      columns,
      count,
      idCustomer: null,
      notes: null,
      isLoading: false,
      dataForm: {
        product_repair: [],
        notes: "",
        id_customer: "",
        id_user: "1fa23b16-6103-7219-563c-3a057c20d389",
      },
    };
  },
  methods: {
    moment,
    handleChange(value, key, column) {
      const newData = [...this.data];
      const target = newData.find((item) => key === item.key);
      if (target) {
        target[column] = value;
        this.data = newData;
      }
    },
    handleAdd() {
      const { count, data } = this;
      const newData = {
        key: count,
        pR_code: ``,
        pR_Name: ``,
        detail_product_pepairs: {
          pD_CPU: 0,
          pD_HDD: 0,
          pD_Ram: 0,
          pD_Wifi: 0,
          pD_Pin: 0,
          pD_Adapter: 0,
          pD_Keyboard: 0,
          pD_PSU: 0,
          pD_LCD: 0,
          text: "",
        },
        pR_repair_type: 0,
        pR_status_error: ``,
        pR_price: 0,
        pR_date_finish: ``,
      };
      this.data = [...data, newData];
      this.count = count + 1;
    },
    onDelete(key) {
      const data = [...this.data];
      this.data = data.filter((item) => item.key !== key);
    },
    onSubmitData(values, keyIndex) {
      this.data[keyIndex].detail_product_pepairs = values;
      console.log(this.data);
    },
    onChangeRepairType(value, keyIndex) {
      this.data[keyIndex].pR_repair_type = value;
      console.log(this.data);
    },
    onChangePrice(value, keyIndex) {
      this.data[keyIndex].pR_price = value;
      console.log(this.data);
    },
    onChangeDateFinish(value, keyIndex) {
      this.data[keyIndex].pR_date_finish = this.moment(value).format();
      console.log(this.data);
    },
    onSelectCustomer(value) {
      this.dataForm.id_customer = value;
    },
    onChangeNote(value) {
      this.dataForm.notes = value;
      console.log(value);
    },
    handleSubmit() {
      const payload = {
        ...this.dataForm,
        product_repair: this.data,
      };

      console.log(payload);

      this.isLoading = true;

      this.$store
        .dispatch("order_repairs/createOrderRepair", payload)
        .then(() => {
          this.isLoading = false;
          this.$message.success("Tạo mới đơn sửa chữa thành công");
          this.$router.push({
            path: "/order_repairs/",
          });
        })
        .catch((err) => {
          this.isLoading = false;
          this.$message.error(err);
        });
    },
  },
};
</script>

<style lang="less" scoped>
#order-repair-form-footer {
  display: flex;
  justify-content: end;
}
.editable-row-operations a {
  margin-right: 8px;
}
</style>
