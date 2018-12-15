<template>
    <div>
        <h1>this is vue.js</h1>
        <p>{{ propOne }}</p>
        <p>{{ propTwo }}</p>
    </div>
</template>
 
<script>
    import Vue from "vue";

    export default {
        data: function () {},
        props: {
            propOne: String,
            propTwo: Number
        }
    };
</script>
 
<style>
    h1 {
        color: #ff0000;
    }
</style>
