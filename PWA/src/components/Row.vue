<template>
  <div class="o-row">
    <slot></slot>
  </div>
</template>

<script>
  export default {
    name: 'Row'
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
  @import '../style/objects/objects.row';
</style>
