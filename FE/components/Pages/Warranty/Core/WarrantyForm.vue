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
        v-for="col in ['pW_code', 'pW_Name', 'pW_status']"
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

      <template slot="warranty_times" slot-scope="text, record, index">
        <repair-price-input
          :keyIndex="index"
          v-on:onChangePrice="onChangeCount"
        />
      </template>

      <template slot="total_count" slot-scope="text, record, index">
        <repair-price-input
          :keyIndex="index"
          v-on:onChangePrice="onChangeTimes"
        />
      </template>

      <template slot="product_warranty_type" slot-scope="text, record, index">
        <repair-type-select
          v-on:onChangeRepairType="onChangeRepairType"
          :keyIndex="index"
          :isRepairSelection="false"
        />
      </template>

      <template slot="pW_date_finish" slot-scope="text, record, index">
        <repair-date-finish-input
          :keyIndex="index"
          v-on:onChangeDateFinish="onChangeDateFinish"
        />
      </template>

      <template slot="detail_product_warranty" slot-scope="text, record, index">
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

import DetailOrderRepairsCreate from "../../OrderRepairs/Actions/DetailOrderRepairsCreate.vue";
import RepairTypeSelect from "../../OrderRepairs/Actions/RepairTypeSelect.vue";
import RepairPriceInput from "../../OrderRepairs/Actions/RepairPriceInput.vue";
import RepairDateFinishInput from "../../OrderRepairs/Actions/RepairDateFinishInput.vue";
import RepairCustomerSelect from "../../OrderRepairs/Actions/RepairCustomerSelect.vue";

const columns = [
  {
    title: "STT",
    dataIndex: "key",
    scopedSlots: { customRender: "key" },
  },
  {
    title: "Mã SP",
    dataIndex: "pW_code",
    scopedSlots: { customRender: "pW_code" },
  },
  {
    title: "Tên SP",
    dataIndex: "pW_Name",
    scopedSlots: { customRender: "pW_Name" },
  },
  {
    title: "Chi tiết máy",
    dataIndex: "detail_product_warranty",
    scopedSlots: { customRender: "detail_product_warranty" },
  },
  {
    title: "Số lượng",
    dataIndex: "total_count",
    scopedSlots: { customRender: "total_count" },
  },
  {
    title: "Tình trạng lỗi",
    dataIndex: "pW_status",
    scopedSlots: { customRender: "pW_status" },
  },
  {
    title: "Số lần BH",
    dataIndex: "warranty_times",
    scopedSlots: { customRender: "warranty_times" },
  },
  {
    title: "Loại BH",
    dataIndex: "product_warranty_type",
    scopedSlots: { customRender: "product_warranty_type" },
  },
  {
    title: "Ngày hẹn trả",
    dataIndex: "pW_date_finish",
    scopedSlots: { customRender: "pW_date_finish" },
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
        product_warranties: [],
        text: "",
        iD_cus: "",
        iD_user: "1fa23b16-6103-7219-563c-3a057c20d389",
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
        pW_code: ``,
        pW_Name: ``,
        detail_product_warranty: {
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
        product_warranty_type: 0,
        pW_status: ``,
        total_count: 0,
        warranty_times: 0,
        pW_date_finish: ``,
      };
      this.data = [...data, newData];
      this.count = count + 1;
    },
    onDelete(key) {
      const data = [...this.data];
      this.data = data.filter((item) => item.key !== key);
    },
    onSubmitData(values, keyIndex) {
      this.data[keyIndex].detail_product_warranty = values;
      console.log(this.data);
    },
    onChangeRepairType(value, keyIndex) {
      this.data[keyIndex].product_warranty_type = value;
      console.log(this.data);
    },
    onChangeCount(value, keyIndex) {
      this.data[keyIndex].total_count = value;
      console.log(this.data);
    },
    onChangeTimes(value, keyIndex) {
      this.data[keyIndex].warranty_times = value;
      console.log(this.data);
    },
    onChangeDateFinish(value, keyIndex) {
      this.data[keyIndex].pW_date_finish = this.moment(value).format();
      console.log(this.data);
    },
    onSelectCustomer(value) {
      this.dataForm.iD_cus = value;
    },
    onChangeNote(value) {
      this.dataForm.text = value;
      console.log(value);
    },
    handleSubmit() {
      const payload = {
        ...this.dataForm,
        product_warranties: this.data,
      };

      console.log(payload);

      this.isLoading = true;

      this.$store
        .dispatch("warranties/createWarranty", payload)
        .then(() => {
          this.isLoading = false;
          this.$message.success("Tạo mới đơn bảo hành thành công");
          this.$router.push({
            path: "/warranty/",
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
