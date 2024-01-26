<template>
    <div class="application-type-select">
        <!-- button to launch the modal dialog -->
        <button @click="$refs.dialogEl.showModal()" type="button" class="btn btn-radio btn-radio--secondary">
            Choose Application Type(s)
        </button>

        <!-- modal dialog with selection options -->
        <dialog ref="dialogEl">
            hello world!

            <button type="button" @click="$refs.dialogEl.close()">close</button>
        </dialog>

        <!-- expandable/removable panels to show the selection -->
        <label class="application_type can-wrap">
            Application type(s) selected:
        </label>

        <div>
            (accordion picker thing here)
        </div>

        <!-- hidden input element to post the selected values with the form -->
        <input type="text" value="test">
    </div>
</template>

<script>
export default {
    name: 'ApplicationTypeSelect',

    data: () => ({
        test: null
    }),
}
</script>

<style lang="scss">
.application-type-select {
    dialog {
        background-color: red;

        &::backdrop {
            background: rgba(0, 0, 0, 0.25);
        }
    }
}
</style>
