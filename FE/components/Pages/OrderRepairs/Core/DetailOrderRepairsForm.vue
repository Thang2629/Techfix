<template>
  <div class="detail-order-repairs-form">
    <a-form-item label="CPU">
      <a-input-number
        v-decorator="[
          'pD_CPU',
          {
            initialValue: 0,
          },
        ]"
        placeholder="CPU"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="HDD">
      <a-input-number
        v-decorator="[
          'pD_HDD',
          {
            initialValue: 0,
          },
        ]"
        placeholder="HDD"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Ram">
      <a-input-number
        v-decorator="[
          'pD_Ram',
          {
            initialValue: 0,
          },
        ]"
        placeholder="Ram"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Wifi">
      <a-input-number
        v-decorator="[
          'pD_Wifi',
          {
            initialValue: 0,
          },
        ]"
        placeholder="Wifi"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Pin">
      <a-input-number
        v-decorator="[
          'pD_Pin',
          {
            initialValue: 0,
          },
        ]"
        placeholder="Pin"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Adapter">
      <a-input-number
        v-decorator="[
          'pD_Adapter',
          {
            initialValue: 0,
          },
        ]"
        placeholder="Adapter"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Keyboard">
      <a-input-number
        v-decorator="[
          'pD_Keyboard',
          {
            initialValue: 0,
          },
        ]"
        placeholder="Keyboard"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="PSU">
      <a-input-number
        v-decorator="[
          'pD_PSU',
          {
            initialValue: 0,
          },
        ]"
        placeholder="PSU"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="LCD">
      <a-input-number
        v-decorator="[
          'pD_LCD',
          {
            initialValue: 0,
          },
        ]"
        placeholder="LCD"
        size="large"
        allow-clear
      />
    </a-form-item>

    <a-form-item label="Chú thích">
      <a-input
        v-decorator="[
          'text',
          {
            initialValue: '',
          },
        ]"
        placeholder="Chú thích"
        size="large"
        allow-clear
      />
    </a-form-item>
  </div>
</template>

<script>
export default {
  name: "DetailOrderRepairsForm",
};
</script>

<style lang="less">
.detail-order-repairs-form {
  .ant-form-item {
    margin-bottom: 1rem;
  }
}
</style>
