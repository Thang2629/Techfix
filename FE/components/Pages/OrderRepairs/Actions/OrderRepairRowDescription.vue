<template>
  <div>
    <a-button @click="showModal">Show</a-button>
    <a-modal
      title="Chi tiết máy"
      :visible="visible"
      @cancel="handleCancel"
      @ok="handleCancel"
      class="fc-modal"
      :destroyOnClose="true"
    >
      <a-descriptions
        bordered
        :column="{ xxl: 4, xl: 3, lg: 3, md: 3, sm: 2, xs: 1 }"
      >
        <a-descriptions-item label="CPU">
          {{ descriptionDetail.pD_CPU }}
        </a-descriptions-item>
        <a-descriptions-item label="HDD">
          {{ descriptionDetail.pD_HDD }}</a-descriptions-item
        >
        <a-descriptions-item label="RAM">
          {{ descriptionDetail.pD_Ram }}
        </a-descriptions-item>
        <a-descriptions-item label="Wifi">
          {{ descriptionDetail.pD_Wifi }}
        </a-descriptions-item>
        <a-descriptions-item label="Pin">
          {{ descriptionDetail.pD_Pin }}
        </a-descriptions-item>
        <a-descriptions-item label="Adapter">
          {{ descriptionDetail.pD_Adapter }}
        </a-descriptions-item>
        <a-descriptions-item label="Keyboard">
          {{ descriptionDetail.pD_Keyboard }}
        </a-descriptions-item>
        <a-descriptions-item label="Psu">
          {{ descriptionDetail.pD_PSU }}
        </a-descriptions-item>
        <a-descriptions-item label="LCD">
          {{ descriptionDetail.pD_LCD }}
        </a-descriptions-item>
        <a-descriptions-item label="Ghi chú">
          {{ descriptionDetail.text }}
        </a-descriptions-item>
      </a-descriptions>
    </a-modal>
  </div>
</template>

<script>
export default {
  name: "OrderRepairRowDescription",
  props: {
    description: {},
  },
  data() {
    return {
      visible: false,
    };
  },
  methods: {
    showModal() {
      this.visible = true;
    },
    handleCancel() {
      this.visible = false;
    },
  },
  mounted() {
    console.log(this.descriptionDetail);
  },
  computed: {
    descriptionDetail() {
      return this.description.detail_product_repair;
    },
  },
};
</script>

<style></style>
