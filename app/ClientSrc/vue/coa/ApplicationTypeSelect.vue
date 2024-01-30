<template>
    <div class="application-type-select">
        <!-- button to launch the modal dialog -->
        <button :disabled="disabled" @click="showModal" type="button" class="btn btn-radio btn-radio--secondary">
            Choose Application Type(s)
        </button>

        <!-- modal dialog with selection options -->
        <dialog ref="dialogEl">
            <div class="title-wrap">
                <h1 class="m-0">Choose application type(s)</h1>
                <button class="btn-close" type="button" @click="$refs.dialogEl.close()">
                    <i class="fas fa-times m-0"></i>
                </button>
            </div>

            <ul class="options list-unstyled">
                <li>
                    <label class="option" v-for="option in formattedOptions" :key="option.id">
                        <input type="checkbox" :value="option.id" v-model="newSelection" />

                        <div class="label-text">
                            <div class="font-weight-bold option-label">
                                {{ option.label }}
                            </div>

                            <div class="font-weight-normal option-definition">
                                {{ option.definition }}
                            </div>
                        </div>
                    </label>
                </li>
            </ul>

            <div class="actions-wrap">
                <span>{{ newSelection.length }} selected</span>

                <button class="btn btn-secondary mt-0" type="button" @click="confirmSelection">
                    Confirm selection
                </button>
            </div>
        </dialog>

        <!-- expandable/removable panels to show the selection -->
        <label class="application_type can-wrap">
            Application type(s) selected:
        </label>

        <div>
            (accordion picker thing here)
        </div>

        <!-- hidden input elements to post the selected values with the form -->
        <input v-for="selectedId in selection" :key="selectedId" type="hidden" name="SelectedApplicationTypes"
            :value="selectedId" />
    </div>
</template>

<script>
export default {
    name: "ApplicationTypeSelect",

    data: () => ({
        selection: [],
        newSelection: [],
    }),

    props: {
        options: Array,
        initialSelection: Array,
        disabled: Boolean,
    },

    computed: {
        formattedOptions() {
            return this.options.map((option) => {
                return {
                    id: String(option.hearingTypeID),
                    label: option.applicationTypeName,
                    definition: option.applicationTypeDefinition,
                };
            });
        },

        displaySelection() {
            return this.formattedOptions
                .filter(option => this.selection.includes(option.id));
        }
    },

    created() {
        // set initial selection
        if (this.initialSelection) {
            this.selection = [...this.initialSelection];
        }
    },

    methods: {
        /**
         * Resets the selection and opens the modal.
         */
        showModal() {
            // reset selection in the modal
            this.newSelection = [...this.selection];

            // show the modal dialog
            this.$refs.dialogEl.showModal();
        },

        /**
         * Updates the selection and closes the modal.
         */
        confirmSelection() {
            this.selection = [...this.newSelection];

            this.$refs.dialogEl.close();
        },
    },
};
</script>

<style lang="scss">
.application-type-select {
    dialog[open] {
        padding: 2em 2em 4em;
        margin: 0;
        display: flex;
        flex-direction: column;
        background-color: #fff;
        border: none;

        // full screen on small devices
        height: 100%;
        width: 100%;
        max-height: 100%;
        max-width: 100%;

        // limit height/width on larger screens
        @media (min-width: 576px) {
            margin: auto;
            height: 750px;
            width: 900px;
            max-height: calc(100% - 2.5em);
            max-width: calc(100% - 2.5em);
        }

        .title-wrap {
            flex: 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 1em;

            h1 {
                font-size: 1.5em;
                line-height: 1;
            }

            .btn-close {
                background: none;
                border: none;
                font-size: 1.5em;
            }
        }

        .options {
            overflow-y: scroll;
            margin-bottom: 1em;

            .option {
                display: flex;
                align-items: baseline;
                line-height: 1;
                min-height: 1.5em;
                margin: 0;
                padding: 0.5em 0;
                border-bottom: 1px solid rgba(0, 0, 0, 0.25);

                input {
                    margin-right: 0.75em;
                }

                .label-text {
                    flex-grow: 1;

                    .option-label {
                        line-height: 1.5;
                    }
                }
            }
        }

        .actions-wrap {
            background-color: #292728;
            color: #fff;
            flex: 0;
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            align-items: center;
            display: flex;
            justify-content: space-between;
            height: 4em;
            padding: 1em;

            .btn-secondary {
                &::after {
                    margin-left: 10px;
                }
            }
        }

        &::backdrop {
            background: rgba(0, 0, 0, 0.25);
        }
    }
}
</style>
